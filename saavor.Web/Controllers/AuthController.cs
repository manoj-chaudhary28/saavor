using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using saavor.Application.CountryState.Query.GetCountryState;
using saavor.Shared.AppSettings;
using saavor.Shared.Constants;
using saavor.Shared.DTO.Login;
using saavor.Shared.DTO.SignUp;
using saavor.Shared.DTO.State;
using saavor.Shared.Interfaces;
using saavor.Shared.Interfaces.SignUp;
using saavor.Shared.ViewModel;
using saavor.Web.Services; 

namespace saavor.Web.Controllers
{/// <summary>
 /// Auth controller
 /// </summary>
    public class AuthController : Controller
    {
        /// <summary>
        /// Get the root folder
        /// </summary>
        private readonly IWebHostEnvironment _environment;
        /// <summary>
        /// Claim service to Add/Get/Remove
        /// </summary>
        private readonly IClaimService _iClaimService;
        /// <summary>
        /// Singup service
        /// </summary>       
        private readonly IUpsertSignupCommand _upsertSignupCommand;
        /// <summary>
        /// Singin service
        /// </summary>
        private readonly IGetLoginQuery _getLoginQuery;
        /// <summary>
        /// Get Country/States
        /// </summary>
        private readonly GetCountryStateQuery _getCountryStateQuery;
        /// <summary>
        /// IHttpContextAccessor to get user
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// IDataProtector
        /// </summary>
        private readonly IDataProtector _protector;
        /// <summary>
        /// DataProtection
        /// </summary>
        private readonly DataProtection _dataProtectionAppSettings;
        /// <summary>
        /// UtilitieService
        /// </summary>
        private readonly UtilitieService _utilitieService;
        /// <summary>
        /// _smtp
        /// </summary>
        private readonly Smtp _smtp;
        /// <summary>
        /// _logger
        /// </summary>
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Constructor to inject services 
        /// </summary>
        /// <param name="envInitiator"></param>
        /// <param name="iClaimServiceInstance"></param>
        /// <param name="upsertSignupCommandInstance"></param>
        /// <param name="getLoginQueryInstance"></param>
        /// <param name="getCountryStateQueryInstance"></param>
        /// <param name="httpContextAccessorInstance"></param>
        public AuthController(IWebHostEnvironment envInitiator
                              ,IClaimService iClaimServiceInstance
                              ,IUpsertSignupCommand upsertSignupCommandInstance
                              ,IGetLoginQuery getLoginQueryInstance
                              ,GetCountryStateQuery getCountryStateQueryInstance
                              ,IHttpContextAccessor httpContextAccessorInstance
                              ,IOptions<DataProtection> dataProtectionAppSettingsInstacne
                              ,IDataProtectionProvider providerInstance
                              ,UtilitieService utilitieServiceInstance
                              ,IOptions<Smtp> smtpInstance
                              ,ILogger<AuthController> logger)
        {
            _environment = envInitiator;
            _iClaimService = iClaimServiceInstance;
            _upsertSignupCommand = upsertSignupCommandInstance;
            _getLoginQuery = getLoginQueryInstance;
            _getCountryStateQuery = getCountryStateQueryInstance;
            _httpContextAccessor = httpContextAccessorInstance;
            _dataProtectionAppSettings = dataProtectionAppSettingsInstacne.Value;
            _protector = providerInstance.CreateProtector(_dataProtectionAppSettings.Key);
            _utilitieService = utilitieServiceInstance;
            _smtp = smtpInstance.Value;
            _logger = logger;
        }
        /// <summary>
        /// Action for sign in
        /// </summary>
        /// <returns></returns>
        public IActionResult SignIn()
        {
            _httpContextAccessor.HttpContext.SignOutAsync();
            return View(new LoginInputDTO());
        }

        /// <summary>
        /// Post for sign in
        /// </summary>
        /// <param name="inputDTO">Param for SignIn</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] LoginInputDTO inputDTO)
        {
            string ss = UtilitieService.Decrypt(Convert.ToString("iBMlikMrLQkoAGO0RIpMhStpAz+LjtgJy+1BW8muA+4="));

            if (!ModelState.IsValid)
            {
                return await Task.Run(() => View(inputDTO));
            }
            else
            {

                try
                {
                    var response = await _getLoginQuery.Login(inputDTO);
                    if (response != null)
                    {
                        if (Convert.ToInt32(response.Status) > 0)
                        {
                            if (UtilitieService.Decrypt(Convert.ToString(response.Password)) == inputDTO.Password)
                            {
                                var sessionToken = await _getLoginQuery.SessionToken(Convert.ToInt64(Convert.ToInt64(response.UserId) > 0 ? response.UserId : response.ProfileId));
                                string encryptedProfileId = string.Empty;
                                if (Convert.ToInt32(response.ProfileId) > 0)
                                    encryptedProfileId = _protector.Protect(response.ProfileId);
                                var userClaims = new List<Claim>()
                            {
                                 new Claim(ClaimTypes.Name, String.Format("{0} {1}", response.FirstName,response.LastName)),
                                 new Claim(ClaimTypes.Email, inputDTO.Email),
                                 new Claim(CommonConstants.SaavorUserEmail, inputDTO.Email),
                                 new Claim(CommonConstants.SaavorUserId,response.UserId),
                                 new Claim(CommonConstants.BusinessName,Convert.ToString(response.BusinessName)),
                                 new Claim(CommonConstants.UserName,String.Format("{0} {1}", response.FirstName,response.LastName)),
                                 new Claim(CommonConstants.KitchenName,Convert.ToString(response.KitchenName)),
                                 new Claim(CommonConstants.ProfileId,response.ProfileId),
                                 new Claim(CommonConstants.EncryptedProfileId,encryptedProfileId),
                                 new Claim(CommonConstants.LoginType,response.LoginType),
                                 new Claim(CommonConstants.IsAprovedKitchenRequest,Convert.ToString(response.IsAprovedKitchenRequest)),
                             };

                                _iClaimService.AddClaim(new ClaimsIdentity(userClaims, CommonConstants.UserIdentity)
                                                        , userClaims);

                                if (response.LoginType == CommonConstants.User)
                                {
                                    CommonVm res = await _getLoginQuery.GetRequestStatus(Convert.ToInt64(response.UserId));
                                    if (res != null)
                                    {
                                        if (Convert.ToInt32(res.Id) > 0)
                                        {
                                            return RedirectToAction("home", "dashboard");
                                        }
                                    }
                                    return RedirectToAction("overview", "kitchen");
                                }
                                else
                                {
                                    return this.RedirectToAction("overview", "kitchendashboard", new { id = encryptedProfileId, name = response.KitchenName });
                                }

                            }
                            else
                            {
                                inputDTO.Message = "Invalid email/password. Please try again!";
                            }
                        }
                        else
                        {
                            inputDTO.Message = response.Message;
                        }
                    }
                    return await Task.Run(() => View(inputDTO));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return await Task.Run(() => View(inputDTO));
                }
            }
        }

        /// <summary>
        /// Render signup view
        /// </summary>
        /// <returns></returns>
        public IActionResult SignUp()
        {
            var model = new SignupInputDTO()
            {
                CountryStateList = BindCountryState(0)

            };
            return View(model);
        }

        /// <summary>
        /// Post for singup 
        /// </summary>
        /// <param name="inputDTO">Param for Singup</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] SignupInputDTO inputDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    inputDTO.CountryStateList = BindCountryState(0);
                    return await Task.Run(() => View(inputDTO));
                }
                else
                {
                    if (inputDTO.Country > 0 && inputDTO.State > 0)
                    {
                        inputDTO.ConfirmPassword = UtilitieService.Encrypt(inputDTO.ConfirmPassword);
                        inputDTO.FileName = inputDTO.BusinessLogo != null ? DateTime.Now.Ticks.ToString() + Path.GetExtension(inputDTO.BusinessLogo.FileName) : string.Empty;
                        var response = await _upsertSignupCommand.Create(inputDTO);
                        if (response != null)
                        {
                            if (Convert.ToInt32(response.Status) > 0)
                            {
                                inputDTO.FileName = inputDTO.BusinessLogo != null ? UploadBusinessLogo(inputDTO) : string.Empty;
                                var userClaims = new List<Claim>()
                            {
                                 new Claim(ClaimTypes.Name, String.Format("{0} {1}", response.FirstName,response.LastName)),
                                 new Claim(ClaimTypes.Email, inputDTO.Email),
                                 new Claim(CommonConstants.SaavorUserId,response.UserId),
                                 new Claim(CommonConstants.BusinessName,response.BusinessName),
                                 new Claim(CommonConstants.UserName,String.Format("{0} {1}", response.FirstName,response.LastName)),
                                 new Claim(CommonConstants.KitchenName,string.Empty),
                                 new Claim(CommonConstants.ProfileId,CommonConstants.Zero),
                                 new Claim(CommonConstants.EncryptedProfileId,string.Empty),
                                 new Claim(CommonConstants.LoginType,CommonConstants.User),
                                 new Claim(CommonConstants.IsAprovedKitchenRequest,Convert.ToString(response.IsAprovedKitchenRequest)),
                             };

                                _iClaimService.AddClaim(new ClaimsIdentity(userClaims, CommonConstants.UserIdentity)
                                                        , userClaims);
                                return RedirectToAction("overview", "kitchen");
                            }
                            else
                            {
                                inputDTO.CountryStateList = BindCountryState(0);
                                inputDTO.Message = response.Message;
                            }
                        }
                    }
                    else
                    {
                        inputDTO.CountryStateList = BindCountryState(0);
                        inputDTO.Message = CommontValidationConstants.SelectStateCountry;
                    }

                }
                return await Task.Run(() => View(inputDTO));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return await Task.Run(() => View(inputDTO));
            }
        }

        /// <summary>
        /// Signout from application
        /// </summary>
        /// <returns></returns>
        public IActionResult SignOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync();
            return RedirectToAction("signin", "auth");
        }

        /// <summary>
        /// Upload business logo
        /// </summary>
        /// <param name="inputDTO">Param for upload business logo</param>
        /// <returns></returns>
        private string UploadBusinessLogo(SignupInputDTO inputDTO)
        {
            string fileName = string.Empty;

            if (inputDTO.BusinessLogo != null)
            {
                string rootFolder = Path.Combine(_environment.WebRootPath, CommonConstants.BusinessLogoFolderName);
                fileName = inputDTO.FileName;
                string filePath = Path.Combine(rootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    inputDTO.BusinessLogo.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        /// <summary>
        /// Get Country/States
        /// </summary>
        /// <returns></returns>
        private CountryStateVm BindCountryState(int countryId)
        {
            return _getCountryStateQuery.GetCountryState(countryId);
        }

        /// <summary>
        /// Forgot password
        /// </summary>
        /// <returns></returns>
        public IActionResult SendEmail()
        {
            return View(new ForgotPasswordSendEmailDTO());
        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] ForgotPasswordSendEmailDTO inputDTO)
        {
            if (!ModelState.IsValid)
            {
                return await Task.Run(() => View(inputDTO));
            }
            var response = await _getLoginQuery.CheckUser(inputDTO.Email);
            var model = new ForgotPasswordSendEmailDTO()
            {
                Message = CommonConstants.EmailSent
            };
            if (Convert.ToInt32(response.Id) > 0)
            {
                string body = string.Empty;
                string html = Path.Combine(_environment.WebRootPath, CommonConstants.ForgotPasswordTemplate);
                using (StreamReader reader = new StreamReader(html))
                {
                    body = reader.ReadToEnd();
                }
                var protectedId = _protector.Protect(response.Id);
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                 body = body.Replace("{firstname}", response.Status);
                 body = body.Replace("{link}", host + "/auth/forgot/" + protectedId);     
                EmailUtility.SendEmail(_smtp.SmtpClient, _smtp.SmtpFromEmail, _smtp.SmtpPassword,inputDTO.Email, CommonConstants.ForgotPassword, body, true);

            }
            return await Task.Run(() => View(model));
        }

        /// <summary>
        /// Forgot
        /// </summary>
        /// <returns></returns>
        public IActionResult Forgot(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                TempData["ForgotUserId"] = _protector.Unprotect(id);
            }
            return View(new ForgotPasswordDTO());
        }

        /// <summary>
        /// Forgot
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Forgot([FromForm] ForgotPasswordDTO inputDTO)
        {
            if (!ModelState.IsValid)
            {
                return await Task.Run(() => View(inputDTO));
            }
            inputDTO.UserId = Convert.ToString(TempData["ForgotUserId"]);
            inputDTO.ConfirmPassword = UtilitieService.Encrypt(inputDTO.ConfirmPassword);
            var response = await _getLoginQuery.ForgotPassword(inputDTO);
            var model = new ForgotPasswordDTO()
            {
                Message = response.Status
            };
            if (Convert.ToInt32(response.Id) > 0)
            {
                return RedirectToAction("signin", "auth");
            }
            return await Task.Run(() => View(model));
        }
        /// <summary>
        /// GetState
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpPost]
        public List<StateDTO> GetState(string countryId)
        {
            try
            {
                return _getCountryStateQuery.GetState(Convert.ToInt32(countryId));
            }
            catch
            {
                return new List<StateDTO>();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <returns></returns>
        /// 
        [Route(CommonConstants.RoutUserProfile)]
        public async Task<IActionResult> Update()
        {
            var response = await _upsertSignupCommand.Get(Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)));           
            SignupUpdateInputDTO model = new SignupUpdateInputDTO()
            {
                FirstName = response.FirstName ?? string.Empty,
                LastName = response.LastName ?? string.Empty,
                BusinessName = response.BusinessName ?? string.Empty,
                Contact = response.Contact ?? string.Empty,
                ContactCountryCode = (!string.IsNullOrEmpty(response.Contact) ? response.Contact.Split("-")[0] : string.Empty ),
                Country = response.CountryId,
                State = response.StateId,
                City = response.City ?? string.Empty,
                Address = response.Address ?? string.Empty,
                TaxId = response.TaxId ?? string.Empty,
                FileName = response.BusinessLogo ?? "no-image.jpg",
                UserId = response.UserId,
                Email = response.Email,
                CountryStateList = BindCountryState(response.CountryId)
            };
            
            return View(model);
        }

        [HttpPost]
        [Route(CommonConstants.RoutUserProfile)]
        public async Task<IActionResult> Update([FromForm] SignupUpdateInputDTO inputDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    inputDTO.CountryStateList = BindCountryState(0);
                    return await Task.Run(() => View(inputDTO));
                }
                else
                {
                    if (inputDTO.Country > 0 && inputDTO.State > 0)
                    {
                        inputDTO.UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId));
                        inputDTO.FileName = inputDTO.BusinessLogo != null ? DateTime.Now.Ticks.ToString() + Path.GetExtension(inputDTO.BusinessLogo.FileName) : string.Empty;
                        if (!string.IsNullOrEmpty(inputDTO.Contact))
                        {
                            if(inputDTO.Contact.Trim().Contains(" "))
                            {
                                inputDTO.Contact = inputDTO.Contact.Replace(" ","-");
                            }
                            else
                            {
                                inputDTO.Contact = inputDTO.ContactCountryCode + "-"+inputDTO.Contact.Split("-");
                            }
                        }
                        var response = await _upsertSignupCommand.Update(inputDTO);
                        if (response != null)
                        {
                            if (Convert.ToInt32(response.Status) > 0)
                            {
                                var bussinessLogo = new SignupInputDTO()
                                {
                                    FileName = inputDTO.FileName,
                                    BusinessLogo = inputDTO.BusinessLogo
                                };
                                inputDTO.FileName = inputDTO.BusinessLogo != null ? UploadBusinessLogo(bussinessLogo) : string.Empty;
                                var userClaims = new List<Claim>()
                                {
                                 new Claim(CommonConstants.SaavorUserId, _iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                                 new Claim(CommonConstants.SaavorUserEmail, _iClaimService.GetClaim(CommonConstants.SaavorUserEmail)),
                                 new Claim(CommonConstants.BusinessName,inputDTO.BusinessName),
                                 new Claim(CommonConstants.UserName,inputDTO.FirstName + " " + inputDTO.LastName),
                                 new Claim(CommonConstants.KitchenName,_iClaimService.GetClaim(CommonConstants.KitchenName)),
                                 new Claim(CommonConstants.ProfileId,_iClaimService.GetClaim(CommonConstants.ProfileId)),
                                 new Claim(CommonConstants.EncryptedProfileId,_iClaimService.GetClaim(CommonConstants.ProfileId) == "0" ? string.Empty : _iClaimService.GetClaim(CommonConstants.ProfileId)),
                                 new Claim(CommonConstants.LoginType,_iClaimService.GetClaim(CommonConstants.LoginType)),
                                 new Claim(CommonConstants.IsAprovedKitchenRequest,_iClaimService.GetClaim(CommonConstants.IsAprovedKitchenRequest))

                                };

                                _iClaimService.AddClaim(new ClaimsIdentity(userClaims, CommonConstants.UserIdentity)
                                                        , userClaims);
                                return RedirectToAction("home", "dashboard");
                            }
                            else
                            {
                                inputDTO.CountryStateList = BindCountryState(0);
                                inputDTO.Message = response.Message;
                            }
                        }
                    }
                    else
                    {
                        inputDTO.CountryStateList = BindCountryState(0);
                        inputDTO.Message = CommontValidationConstants.SelectStateCountry;
                    }

                }
                return await Task.Run(() => View(inputDTO));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return await Task.Run(() => View(inputDTO));
            }
        }
    }
}
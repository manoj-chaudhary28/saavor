using System;


namespace saavor.Shared.Filter
{
    public static class SetPaging
    {
        public static string Set_Paging(Int32 PageNumber, int PageSize, Int64 TotalRecords, string ClassName, string PageUrl, string DisableClassName,string Search)
        {
            string ReturnValue = "";
            string additionalParam = string.Empty;
            if(!string.IsNullOrEmpty(Search))
            {
                additionalParam = "&search=" + Search + "";
            }
            try
            {
                Int64 TotalPages = Convert.ToInt64(Math.Ceiling((double)TotalRecords / PageSize));
                if (PageNumber > 1)
                {
                    if (PageNumber == 2)
                        ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim() + "?pageNumber=" + Convert.ToString(PageNumber - 1) + additionalParam + "' class='" + ClassName + "'><span class='material-icons'>keyboard_arrow_left</span ></a>";
                    else
                    {
                        ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                        if (PageUrl.Contains("?"))
                            ReturnValue = ReturnValue + "&";
                        else
                            ReturnValue = ReturnValue + "?";
                        ReturnValue = ReturnValue + "pageNumber=" + Convert.ToString(PageNumber - 1) + additionalParam + "' class='" + ClassName + "'><span class='material-icons'>keyboard_arrow_left</span ></a>";
                    }
                }
                else
                    ReturnValue = ReturnValue + "<span class='" + DisableClassName + "'><span class='material-icons'>keyboard_arrow_left</span ></span>";
                if ((PageNumber - 3) > 1)
                    ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim() + "' class='" + ClassName + "'>1</a> ..";
                for (int i = PageNumber - 3; i <= PageNumber; i++)
                    if (i >= 1)
                    {
                        if (PageNumber != i)
                        {
                            ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                            if (PageUrl.Contains("?"))
                                ReturnValue = ReturnValue + "&";
                            else
                                ReturnValue = ReturnValue + "?";
                            ReturnValue = ReturnValue + "pageNumber=" + i.ToString() + additionalParam + "' class='" + ClassName + "'>" + i.ToString() + "</a>";
                        }
                        else
                        {
                            ReturnValue = ReturnValue + "<span class='active_page'>" + i + "</span>";
                        }
                    }
                for (int i = PageNumber + 1; i <= PageNumber + 3; i++)
                    if (i <= TotalPages)
                    {
                        if (PageNumber != i)
                        {
                            ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                            if (PageUrl.Contains("?"))
                                ReturnValue = ReturnValue + "&";
                            else
                                ReturnValue = ReturnValue + "?";
                            ReturnValue = ReturnValue + "pageNumber=" + i.ToString() + additionalParam + "' class='" + ClassName + "'>" + i.ToString() + "</a>";
                        }
                        else
                        {
                            ReturnValue = ReturnValue + "<span class='active_page'>" + i + "</span>";
                        }
                    }
                if ((PageNumber + 3) < TotalPages)
                {
                    ReturnValue = ReturnValue + "..<a href='" + PageUrl.Trim();
                    if (PageUrl.Contains("?"))
                        ReturnValue = ReturnValue + "&";
                    else
                        ReturnValue = ReturnValue + "?";
                    ReturnValue = ReturnValue + "pageNumber=" + TotalPages.ToString()+ additionalParam + "' class='" + ClassName + "'>" + TotalPages.ToString() + "</a>";
                }
                if (PageNumber < TotalPages)
                {
                    ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                    if (PageUrl.Contains("?"))
                        ReturnValue = ReturnValue + "&";
                    else
                        ReturnValue = ReturnValue + "?";
                    ReturnValue = ReturnValue + "pageNumber=" + Convert.ToString(PageNumber + 1) + additionalParam + "' class='" + ClassName + "'><span class='material-icons'>keyboard_arrow_right</span></a>";
                }
                else
                    ReturnValue = ReturnValue + "<span class='" + DisableClassName + "'><span class='material-icons'>keyboard_arrow_right</span></span>";
            }
            catch (Exception ex)
            {
            }
            return (ReturnValue);
        }
        public static string Set_Paging_Dates(Int32 PageNumber, int PageSize, Int64 TotalRecords, string ClassName, string PageUrl, string DisableClassName, string fromDate,string toDate)
        {
            string ReturnValue = "";
            string additionalParam = string.Empty;
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                additionalParam = "&fromDate=" + fromDate+ "&toDate="+ toDate + "";
            }
            try
            {
                Int64 TotalPages = Convert.ToInt64(Math.Ceiling((double)TotalRecords / PageSize));
                if (PageNumber > 1)
                {
                    if (PageNumber == 2)
                        ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim() + "?pageNumber=" + Convert.ToString(PageNumber - 1) + additionalParam + "' class='" + ClassName + "'><span class='material-icons'>keyboard_arrow_left</span ></a>";
                    else
                    {
                        ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                        if (PageUrl.Contains("?"))
                            ReturnValue = ReturnValue + "&";
                        else
                            ReturnValue = ReturnValue + "?";
                        ReturnValue = ReturnValue + "pageNumber=" + Convert.ToString(PageNumber - 1) + additionalParam + "' class='" + ClassName + "'><span class='material-icons'>keyboard_arrow_left</span ></a>";
                    }
                }
                else
                    ReturnValue = ReturnValue + "<span class='" + DisableClassName + "'><span class='material-icons'>keyboard_arrow_left</span ></span>";
                if ((PageNumber - 3) > 1)
                    ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim() + "' class='" + ClassName + "'>1</a> ..";
                for (int i = PageNumber - 3; i <= PageNumber; i++)
                    if (i >= 1)
                    {
                        if (PageNumber != i)
                        {
                            ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                            if (PageUrl.Contains("?"))
                                ReturnValue = ReturnValue + "&";
                            else
                                ReturnValue = ReturnValue + "?";
                            ReturnValue = ReturnValue + "pageNumber=" + i.ToString() + additionalParam + "' class='" + ClassName + "'>" + i.ToString() + "</a>";
                        }
                        else
                        {
                            ReturnValue = ReturnValue + "<span class='active_page'>" + i + "</span>";
                        }
                    }
                for (int i = PageNumber + 1; i <= PageNumber + 3; i++)
                    if (i <= TotalPages)
                    {
                        if (PageNumber != i)
                        {
                            ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                            if (PageUrl.Contains("?"))
                                ReturnValue = ReturnValue + "&";
                            else
                                ReturnValue = ReturnValue + "?";
                            ReturnValue = ReturnValue + "pageNumber=" + i.ToString() + additionalParam + "' class='" + ClassName + "'>" + i.ToString() + "</a>";
                        }
                        else
                        {
                            ReturnValue = ReturnValue + "<span class='active_page'>" + i + "</span>";
                        }
                    }
                if ((PageNumber + 3) < TotalPages)
                {
                    ReturnValue = ReturnValue + "..<a href='" + PageUrl.Trim();
                    if (PageUrl.Contains("?"))
                        ReturnValue = ReturnValue + "&";
                    else
                        ReturnValue = ReturnValue + "?";
                    ReturnValue = ReturnValue + "pageNumber=" + TotalPages.ToString() + additionalParam + "' class='" + ClassName + "'>" + TotalPages.ToString() + "</a>";
                }
                if (PageNumber < TotalPages)
                {
                    ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                    if (PageUrl.Contains("?"))
                        ReturnValue = ReturnValue + "&";
                    else
                        ReturnValue = ReturnValue + "?";
                    ReturnValue = ReturnValue + "pageNumber=" + Convert.ToString(PageNumber + 1) + additionalParam + "' class='" + ClassName + "'><span class='material-icons'>keyboard_arrow_right</span></a>";
                }
                else
                    ReturnValue = ReturnValue + "<span class='" + DisableClassName + "'><span class='material-icons'>keyboard_arrow_right</span></span>";
            }
            catch (Exception ex)
            {
            }
            return (ReturnValue);
        }

    }
}

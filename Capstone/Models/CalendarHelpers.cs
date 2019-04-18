using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;



namespace Capstone.Models
{
    public static class CalendarHelpers
    {
        //Google Calender

        public static string GoogleCalendar(this HtmlHelper helper, string linkText, Event eventItem, string attributes)
        {
            //parse dates
            var dates = eventItem.Beginning.ToString("yyyyMMddTHHmmssZ");
            if ((eventItem.Ending != null) && eventItem.Ending > eventItem.Beginning)
            {
                dates += "/" + eventItem.Ending.ToString("yyyyMMddTHHmmssZ");
            }
            else
            {
                dates += "/" + eventItem.Beginning.ToString("yyyyMMddTHHmmssZ");
            }

            var path = string.Format("http://www.google.com/calendar/event?action=TEMPLATE&text={0}&dates={1}&details={2}&location={3}",
                                            eventItem.EventName,
                                            dates,
                                            eventItem.DescriptionCopy,
                                            eventItem.VenueID                                           
                                            );

            var calendar = string.Format("<a href='{0}' target='_blank' {1}>{2}</a>",
                                            HttpUtility.UrlPathEncode(path),
                                            helper.Encode(attributes),
                                            linkText);

            return calendar;
        }

            
        ////Yahoo CalendarHelpers

        //public static string YahooCalendar(this HtmlHelper helper, string linkText, string what, DateTime start, DateTime? end, string description, string venue, string street, string city, string attributes)
        //{
        //    //parse duration
        //    var duration = "";
        //    if (end.HasValue && end > start)
        //    {
        //        var span = (TimeSpan)(end - start);
        //        duration = "&dur=" + span.ToString("HHMM");
        //    }

        //    var path = string.Format("http://calendar.yahoo.com/?v=60&view=d&type=10&title={0}&st={1}{2}&desc={3}&in_loc={4}&in_st={5}&in_csz={6}'",
        //                        what,
        //                        start.ToString("yyyyMMddTHHmmssZ"),
        //                        duration,
        //                        description,
        //                        venue,
        //                        street,
        //                        city);

        //    var calendar = string.Format("<a href='{0}' target='_blank' {1}>{2}</a>",
        //                                    HttpUtility.UrlPathEncode(path),
        //                                    helper.Encode(attributes),
        //                                    linkText);

        //    return calendar;
        //}

       
    }
}

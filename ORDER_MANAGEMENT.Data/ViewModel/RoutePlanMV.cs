using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Data
{
    public class RoutePlanMV
    {
        [Required]
        public string RouteName { get; set; }
        [Required]
        public string PlanType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<string> SelectedDays { get; set; }
        public IEnumerable<SelectListItem> WeekDays
        {
            get
            {
                var list = new List<SelectListItem>();
                string DayString = "Sat,Sun,Mon,Tue,Wed,Thu,Fri";
                string[] Days = DayString.Split(',');
                if (SelectedDays == null) SelectedDays = new List<string>();
                foreach (string day in Days)
                {
                    list.Add(new SelectListItem { Text = day, Value = day, Selected = SelectedDays.Contains(day) });
                }

                return list;
            }
        }

    }
    public class UserRoutePoint
    {
        public UserRoutePoint()
        {
            this.points = new List<Point>();
        }
        public int RegistrationID { get; set; }
        public int RouteID { get; set; }
        public List<Point> points { get; set; }
    }
    public class Point
    {
        public int? ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class UserRouteVM
    {
        public int RegistrationID { get; set; }
        public string UserName { get; set; }
        public int? RouteID { get; set; }
        public RoutePlanMV RouteDetails { get; set; }
        public List<Point> points { get; set; }

    }



}

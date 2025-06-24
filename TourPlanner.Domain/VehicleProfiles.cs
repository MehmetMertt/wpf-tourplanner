using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Domain
{
    public enum VehicleProfile
    {
        // Driving Profiles
        [Description("driving-car")]
        DrivingCar,

        [Description("driving-hgv")] // Heavy Goods Vehicle (Trucks)
        DrivingHGV,

        // Cycling Profiles
        [Description("cycling-regular")] // Regular bicycle
        CyclingRegular,

        [Description("cycling-road")] // Road bicycle 
        CyclingRoad,

        [Description("cycling-mountain")] // Mountain bike 
        CyclingMountain,

        [Description("cycling-electric")] // Electric bicycle 
        CyclingElectric,

        // Walking/Hiking Profiles
        [Description("foot-walking")] // Standard walking
        FootWalking,

        [Description("foot-hiking")] // Hiking 
        FootHiking,

        // Wheelchair Profile
        [Description("wheelchair")] // Routes optimized for accessibility
        Wheelchair
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return value.ToString();
            }

            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute?.Description ?? value.ToString();
        }
    }
}

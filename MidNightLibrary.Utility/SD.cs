using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidNightLibrary.Utility
{
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Admin = "Admin";


        public static string OrderPending { get; set; } = "Pending";
        public static string OrderApproved { get; set; } = "Approved";
        public static string OrderFailed { get; set; } = "Failed";
        public static string OrderProcessing { get; set; } = "Processing";
        public static string OrderShipping { get; set; } = "Shipping";
        public static string OrderDelivered { get; set; } = "Delivered";

        public static string PaymentPending { get; set; } = "Pending";
        public static string PaymentFailed { get; set; } = "Failed";
        public static string PaymentApproved { get; set; } = "Approved";




    }
}

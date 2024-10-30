namespace HelpyWebApi.Controllers.Helper
{
    public class BillHelper
    {
        public static int MapBillToId(string item)
        {
            // Example mapping. Customize as needed.
            var billMapping = new Dictionary<string, int>
            {
                { "Travel/Transportation", 1 },
                { "Dining out", 2 },
                { "Emergency Expense", 3 },
                { "Personal care products", 4 },
                { "Student loan payment", 5 },
                { "School Supplies", 6 }
            };

            return billMapping.ContainsKey(item) ? billMapping[item] : 0;
        }
    }
}

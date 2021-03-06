namespace WebsocketDemo.Services
{
    public class OrderService
    {
        private int index;

        private readonly string[] Status =
            { "New", "Pending payment", "Awaiting Shipment" ,   "Completed"};


        public CheckResult GetUpdate(int orderNo)
        {
            if (index >= 4)
                return new CheckResult { New = false, Finished = true };


            var result = new CheckResult
            {
                New = true,
                Update = Status[index],
                Finished = Status.Length - 1 == index
            };
            index++;

            return result;
        }
    }

    public class CheckResult
    {
        public bool New { get; set; }
        public string Update { get; set; }
        public bool Finished { get; set; }
    }
}

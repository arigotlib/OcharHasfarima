namespace OcharHasfarim.DAL
{
    public class Data
    {
        private string connctionString = "server = ARI-GOTLIB\\SQLSERVER; initial catalog = MyFriends; user id = sa; password=1234;TrustServerCertificate=Yes";
        static Data _data;
        private DBContext _layer;

        private Data()
        {
            _layer = new DBContext(connctionString);
        }
        public static DBContext Get
        {
            get
            {
                if (_data == null)
                {
                    _data = new Data();
                }
                return _data._layer;
            }
        }
    }
}
namespace MoneyManagement.DomainModel.Commons
{
    public class BaseEntity:Entity
    {
        private string _shortName;

        public string ShortName
        {
            get { return _shortName?? "NONE";}
            set { _shortName = value ?? "NONE"; }
        }
    }
}

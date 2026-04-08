using MyFamilyRoot.Models;

namespace MyFamilyRoot.Services
{
    public interface IFamily
    {
        public string Constring();

        public List<Familymodel> Getalldata();
        public bool InsertData(Familymodel model);

        public bool DeleteData(Familymodel model);

        public List<Familymodel> fetchingdatabyid(Familymodel model);

        public bool UpdateData(Familymodel model);
    }

}

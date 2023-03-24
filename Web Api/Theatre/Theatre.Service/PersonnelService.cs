using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theatre.Model;
using Theatre.Repository;
using Theatre.Service.Common;

namespace Theatre.Service
{
    public class PersonnelService : IPersonnelService
    {
        public List<Personnel> GetAllPersonnel()
        {
            PersonnelRepository repository = new PersonnelRepository();
            List<Personnel> worker = repository. GetAllPersonnel();
            return worker;
        }
        public List<Personnel>GetPersonnel(string surname)
        {
            PersonnelRepository repository = new PersonnelRepository();
            List<Personnel> personnel = repository. GetAllPersonnel();
            return personnel;
        }
        public bool AddPersonnel(Personnel personnel)
        {
            PersonnelRepository repository = new PersonnelRepository();
            bool newPersonnel = repository.AddPersonnel(personnel);
            return true;
        }
        public bool EditPersonnel(Guid id,  Personnel personnel)
        {
            PersonnelRepository repository = new PersonnelRepository();
            bool newPersonnel = repository.AddPersonnel(personnel);
            return true;
        }
        public bool DeletePersonnel(Guid id)
        {
            PersonnelRepository repository = new PersonnelRepository();
            bool personnel = repository.DeletePersonnel(id);
            return true;
        }
    }
}

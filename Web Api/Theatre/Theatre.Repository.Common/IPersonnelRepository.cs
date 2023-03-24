using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theatre.Model;

namespace Theatre.Repository.Common
{
    public interface IPersonnelRepository
    {
        List<Personnel> GetAllPersonnel();
        List<Personnel> GetPersonnel(string surname);
        bool AddPersonnel(Personnel personnel);
        bool EditPersonnel(Guid id, Personnel personnel);
        bool DeletePersonnel(Guid id);
    }
}

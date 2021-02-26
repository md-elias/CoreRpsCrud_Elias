using CoreRpsCrud_Elias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRpsCrud_Elias.Interfaces
{
    public interface ITraineeRepository
    {
        Trainee GetTrainee(int id);

        IEnumerable<Trainee> GetAll();

        Trainee Add(Trainee trainee);
        Trainee Update(Trainee trainee);
        Trainee Delete(int id);
    }
}

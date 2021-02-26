using CoreRpsCrud_Elias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRpsCrud_Elias.Interfaces
{
    public interface ICourseRepository
    {
        Course GetCourse(int id);

        IEnumerable<Course> GetAll();

        Course Add(Course course);
        Course Update(Course course);
        Course Delete(int id);
    }
}

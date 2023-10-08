using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Comman
{

    public enum TaskStatus
    {
        delevired,
        completed
    }
    public enum Roles
    {
        Admin,
        Basic
    }
    public enum Modules
    {
        Employee,
        Department,
        EmployeeTask,
    }
  public enum CRUD_OperationType
  {
    Create,
    Read,
    Update,
    Delete
  }

}

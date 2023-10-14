using AribTask.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Comman
{
    public static class Extensions
    {
        public static void SetBasicData(this BaseEntity Entity , CRUD_OperationType Type)
        {
            if (Type== CRUD_OperationType.Create)
            {
                Entity.CreationDate = DateTime.Now;

            }
            if (Type == CRUD_OperationType.Update)
            {
                Entity.UpdatedDate = DateTime.Now;

            }
        }

        
      
    }
}

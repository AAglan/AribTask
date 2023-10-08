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

        
        //public static int GetLastCode(this BaseEntity entity, IBaseRepo<BaseEntity> repo)
        //{
        //    int LastCode = 0;
        //    var code = repo.GetAll().OrderBy(o=>o.Code).LastOrDefault();
        //    if (code != null)
        //    {
        //        LastCode = code.Code + 1;
        //    }
        //    else
        //    {
        //        LastCode = 1;
        //    }
        //    return LastCode;
        //}
        public static bool IsExistCode(this BaseEntity entity, IBaseRepo<BaseEntity> repo)
        {
            bool IsExist = false;
            var code = repo.GetById(entity.Id);
            if (code != null)
            {
                IsExist = true;
            }        
            return IsExist;
        }

        public static List<SelectListItem> ConvertEnumToSelectListItems(Type t)
        {
            var x =new List<SelectListItem>();            
            var elements = Enum.GetValues(t);
            for (int i = 0; i < elements.Length; i++)
            {
                x.Add(new SelectListItem() {Text= t.Name+"."+ elements.GetValue(i).ToString(), Value=i.ToString()} );

            }          
            return x;
        }
    }
}

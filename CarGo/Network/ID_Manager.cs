using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    public class ID_Manager
    {
        private int clientNumber;
        private bool clientNumberSet = false;
        public void SetClientNumber(int number){
            if (number > 0 && number < 10)
            {
                clientNumber = number; 
                clientNumberSet = true;
                lastID = clientNumber * IDRangeSize;
                usedIDs = new HashSet<int>();
                usedIDs.Add(lastID);
            }

            
            
        }

        private HashSet<int> usedIDs;
        private int lastID;
        private int IDRangeSize = 1000000;
        
        private static ID_Manager instance;
        public static ID_Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ID_Manager();
                }
                return instance;
            }
        }

        

        private ID_Manager()
        {
            
        }

        

        public int GetID()
        {
            if(!clientNumberSet)
            {
                return -2;
                //client number not yet set. request it or wait for response
            }
            else
            {
                int newID = lastID + 1;
                if (usedIDs.Contains(newID) || newID % IDRangeSize != lastID % IDRangeSize)
                {
                    //Error wrong ID
                    return -1;
                }
                else
                {
                    usedIDs.Add(newID);
                    lastID = newID;
                    return newID;
                }
            }
            
        }

        //public void RegisterID(int newID)
        //{
        //    if(!usedIDs.Contains(newID))
        //    {
        //        usedIDs.Add(newID);
        //    }
        //}

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TaskIT.Models;

namespace TaskIT.Controllers
{
    public class HomeController:Controller
    {
        public static IList<FirstTable> _table;

        public IActionResult Index()
        {
            IList items = null;
            using(var context = new TaskItContext())
            {
                 items = context.FirstTables.ToList();
            }
            return View(items);
        }

        public IActionResult EditRecord(int id)
        {
            using(var context = new TaskItContext())
            {
                var record = context.FirstTables.FirstOrDefault(r => r.Id == id);
                return View(record);
            }
         
        }

        public IActionResult DeleteRecord(int id)
        {
            using(var context = new TaskItContext())
            {
                var record = context.FirstTables.FirstOrDefault(r => r.Id == id);
                context.FirstTables.Remove(record);
                context.SaveChanges();
                return RedirectToAction("Index");               

            }          
        }

        [HttpPost]
        public IActionResult SaveChanges(int Id ,DateTime NewRecordDate,int NewBranchId,int NewCropYear,int NewCounterpartyId,string NewCounterpartyName,int NewContactId,string NewProduct,int NewPrice,int NewAmount,string NewProcess,int NewWetness,int NewGarbage,string NewInfection)
        {
            using(var context = new TaskItContext())
            {

                var record = context.FirstTables.Find(Id);             
                record.RecordDate = NewRecordDate;
                record.BranchId = NewBranchId;
                record.CropYear = NewCropYear;
                record.CounterpartyId = NewCounterpartyId;
                record.CounterpartyName = NewCounterpartyName;
                record.ContactId = NewContactId;
                record.Product = NewProduct;
                record.Price = NewPrice;
                record.Amount = NewAmount;
                record.Process = NewProcess;
                record.Wetness = NewWetness;
                record.Garbage = NewGarbage;
                record.Infection = NewInfection;
                context.SaveChanges();
               

                return RedirectToAction("Index");
            }
        }

        public IActionResult AddRecord()
        {
            using(var context = new TaskItContext())
            {
                return View(context.FirstTables.OrderBy(r => r.Id).Last().Id + 1);
            }
            
        }

        [HttpPost]
        public IActionResult AddRecordItem(int Id,DateTime? RecordDate, int BranchId,int CropYear,int CounterpartyId,string CounterpartyName,int ContactId,string Product,int Price,int Amount,string Process,int Wetness,int Garbage,string Infection)
        {

            if(CounterpartyName == null || Product == null || Process == null)
            {
                return View("ErrorPage");
            }
            using(var context = new TaskItContext())
            {
                var record = new FirstTable();
                record.Id = Id;
                record.BranchId = BranchId;
                record.CropYear = CropYear;
                record.CounterpartyId = CounterpartyId;
                record.CounterpartyName = CounterpartyName;
                record.ContactId = ContactId;
                record.Product = Product;
                record.Price = Price;
                record.Amount = Amount;
                record .Process = Process;
                record .Wetness = Wetness;
                record .Garbage = Garbage;
                record .Infection = Infection;
                context.FirstTables.Add(record);
                context.SaveChanges();
            }


            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult ProcessSelectedPeriod(DateTime? startDate,DateTime? endDate)
        {
            if(startDate == null && endDate == null)
            {
                 return CreateTablesByDate(DateTime.Now);
            }
            else if(startDate == null && endDate != null)
            {
                return CreateTablesByDate(endDate.Value);
            }
            else if(startDate != null && endDate == null)
            {
                return CreateTablesByDate(startDate.Value);
            }

            return CreateTablesByPeriod(startDate.Value,endDate.Value);
               
        }



        public IActionResult CreateTablesByDate(DateTime date)
        {            
            IList<FirstTable> secondTable = null;
            IList<FirstTable> givenDateRecords = new List<FirstTable>();
            bool isMatch = false;

            using(var context = new TaskItContext())
            {
                secondTable = context.FirstTables.ToList();

                foreach(var record in secondTable)
                {
                    if(record.RecordDate == date)
                    {
                        isMatch = true;
                        givenDateRecords.Add(record);
                    }
                }
                if(isMatch == false)
                {
                    return View("ErrorPage");
                }

                secondTable = CreateSecondTable(givenDateRecords,secondTable);
                _table = givenDateRecords;
            }

            return View("IntermediateData",secondTable);
        }



        public IActionResult CreateTablesByPeriod(DateTime startDate , DateTime endDate)
        {
            IList<FirstTable> secondTable = null;
            IList<FirstTable> givenDateRecords = new List<FirstTable>();
            bool isMatch = false;

            using(var context = new TaskItContext())
            {
                secondTable = context.FirstTables.ToList();

                foreach(var record in secondTable)
                {
                    if(record.RecordDate >= startDate && record.RecordDate <= endDate)
                    {
                        isMatch = true;
                        givenDateRecords.Add(record);
                    }
                }
                if(isMatch == false)
                {
                    return View("ErrorPage");
                }

                secondTable = CreateSecondTable(givenDateRecords,secondTable);
                _table = givenDateRecords;
            }

            return View("IntermediateData",secondTable);
        }




        public IList<FirstTable> CreateSecondTable(IList<FirstTable> givenDateRecords,IList<FirstTable> secondTable)
        {
            for(int i = 0;i < givenDateRecords.Count;i++)
            {
                for(int j = i + 1;j < givenDateRecords.Count;j++)
                {
                    if(givenDateRecords[i].BranchId == givenDateRecords[j].BranchId && givenDateRecords[i].CropYear == givenDateRecords[j].CropYear &&
                        givenDateRecords[i].CounterpartyId == givenDateRecords[j].CounterpartyId && givenDateRecords[i].CounterpartyName == givenDateRecords[j].CounterpartyName &&
                        givenDateRecords[i].ContactId == givenDateRecords[j].ContactId && givenDateRecords[i].Product == givenDateRecords[j].Product &&
                        givenDateRecords[i].Price == givenDateRecords[j].Price && givenDateRecords[i].Process == givenDateRecords[j].Process && givenDateRecords[i].Infection == givenDateRecords[j].Infection &&
                        givenDateRecords[i].Garbage == givenDateRecords[j].Garbage && givenDateRecords[i].Wetness == givenDateRecords[j].Wetness)
                    {


                        givenDateRecords[i].Amount += givenDateRecords[j].Amount;


                        givenDateRecords.Remove(givenDateRecords[j]);
                        secondTable[i] = givenDateRecords[i];
                        secondTable.Remove(secondTable[j]);

                    }

                }
            }

            

            return secondTable;
        }





        public IActionResult CreateThirdTable()
        {

            IList<FirstTable> thirdTable = _table; 

            for(int i = 0;i < thirdTable.Count;i++)
            {
                for(int j = i + 1;j < thirdTable.Count;j++)
                {
                    if(thirdTable[i].RecordDate == thirdTable[j].RecordDate && thirdTable[i].BranchId == thirdTable[j].BranchId &&
                        thirdTable[i].CounterpartyId == thirdTable[j].CounterpartyId && thirdTable[i].CounterpartyName == thirdTable[j].CounterpartyName &&
                        thirdTable[i].ContactId == thirdTable[j].ContactId && thirdTable[i].Product == thirdTable[j].Product &&
                        thirdTable[i].Price == thirdTable[j].Price && thirdTable[i].Process == thirdTable[j].Process)
                    {
                        thirdTable[i].Amount += thirdTable[j].Amount;
                        thirdTable[i].Wetness = (thirdTable[i].Wetness + thirdTable[j].Wetness)/ 2;
                        thirdTable[i].Garbage = (thirdTable[i].Garbage + thirdTable[j].Garbage) / 2;

                        _table.Remove(thirdTable[j]);

                    }

                }
            }

            return View("LastTable",thirdTable);

        }
            

    }
}
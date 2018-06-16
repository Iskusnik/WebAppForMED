[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebAppForMED.MVCGridConfig), "RegisterGrids")]

namespace WebAppForMED
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    using MVCGrid.Models;
    using MVCGrid.Web;

    public static class MVCGridConfig 
    {
        public static void RegisterGrids()
        {
            MVCGridDefinitionTable.Add("PatientGrid", new MVCGridBuilder<Models.Patient>()
       .WithAuthorizationType(AuthorizationType.AllowAnonymous)
       .AddColumns(cols =>
       {
           cols.Add("Id").WithValueExpression(p => p.Id.ToString());

           cols.Add("ФИО").WithHeaderText("ФИО")
               .WithValueExpression(p => p.FIO);

           cols.Add("ДатаРождения").WithHeaderText("Дата рождения")
               .WithValueExpression(p => p.BirthDate.ToString());
       })
       .WithRetrieveDataMethod((options) =>
       {
           var result = new QueryResult<Models.Patient>();
           using (var db = new WebAppForMED.Models.ModelMEDContainer())
           {
               result.Items = db.PatientSet.Where(p => p.Employee).ToList();
           }
           return result;
       })
         );

            /*
            MVCGridDefinitionTable.Add("UsageExample", new MVCGridBuilder<YourModelItem>()
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    // Add your columns here
                    cols.Add().WithColumnName("UniqueColumnName")
                        .WithHeaderText("Any Header")
                        .WithValueExpression(i => i.YourProperty); // use the Value Expression to return the cell text for this column
                    cols.Add().WithColumnName("UrlExample")
                        .WithHeaderText("Edit")
                        .WithValueExpression((i, c) => c.UrlHelper.Action("detail", "demo", new { id = i.Id }));
                })
                .WithRetrieveDataMethod((context) =>
                {
                    // Query your data here. Obey Ordering, paging and filtering parameters given in the context.QueryOptions.
                    // Use Entity Framework, a module from your IoC Container, or any other method.
                    // Return QueryResult object containing IEnumerable<YouModelItem>

                    return new QueryResult<YourModelItem>()
                    {
                        Items = new List<YourModelItem>(),
                        TotalRecords = 0 // if paging is enabled, return the total number of records of all pages
                    };

                })
            );
            */
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace WebAppForMED.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ModelMEDContainer>
    {
        protected override void Seed(ModelMEDContainer context)
        {
            var db = new ModelMEDContainer();

        }
    }
}
using BookStore2.Models;
using BookStore2.Models.Abstract;
using BookStore2.Serveces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore2.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IEmailSender>().To<EmailSender>();
            Bind(typeof(IRepository<>)).To(typeof(EFRepository<>));
        }
    }
}
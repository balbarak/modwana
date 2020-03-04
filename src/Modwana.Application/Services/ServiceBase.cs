using Modwana.Core.Exceptions;
using Modwana.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modwana.Application.Services
{
    public class ServiceBase<TService> where TService : class, new()
    {
        private static Lazy<TService> _instance = new Lazy<TService>(() => new TService());

        public static TService Instance
        {
            get { return _instance.Value; }
        }

        protected internal GenericRepository _repository;

        protected string[] Includes { get; set; }
        
        protected ServiceBase()
        {
            this._repository = new GenericRepository();
        }

        static ServiceBase()
        {

        }

        public void ValidateEntity(object entity)
        {
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
            {
                var erros = new List<string>();

                foreach (var item in validationResults)
                    erros.Add(item.ErrorMessage);

                throw new BusinessException(erros);
            }
        }

    }
}

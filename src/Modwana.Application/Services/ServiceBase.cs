using Modwana.Core.Exceptions;
using Modwana.Core.Interfaces;
using Modwana.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modwana.Application.Services
{
    public class ServiceBase
    {
        protected internal IGenericRepository _repository;

        protected string[] Includes { get; set; }
        
        protected ServiceBase(IGenericRepository repository)
        {
            _repository = repository;   
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

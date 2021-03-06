using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Threax.AspNetCore.Models;
using Threax.AspNetCore.Tracking;
using DevApp.InputModels;
using DevApp.Database;
using DevApp.ViewModels;

namespace DevApp.Mappers
{
    public partial class ValueProfile : Profile
    {
        public ValueProfile()
        {
            //Map the input model to the entity
            MapInputToEntity(CreateMap<ValueInput, ValueEntity>());

            //Map the entity to the view model.
            MapEntityToView(CreateMap<ValueEntity, Value>());
        }

        partial void MapInputToEntity(IMappingExpression<ValueInput, ValueEntity> mapExpr);

        partial void MapEntityToView(IMappingExpression<ValueEntity, Value> mapExpr);
    }
}
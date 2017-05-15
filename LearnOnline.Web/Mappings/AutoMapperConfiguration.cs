using AutoMapper;
using LearnOnline.Model.Models;
using LearnOnline.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnOnline.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Subject, SubjectViewModel>();
                cfg.CreateMap<Level, LevelViewModel>();
                cfg.CreateMap<DetailExam, DetailExamViewModel>();
                cfg.CreateMap<Province, ProvinceViewModel>();
                cfg.CreateMap<District, DistrictViewModel>();
                cfg.CreateMap<UserGroup, UserGroupViewModel>();
            });
        }      
    }
}
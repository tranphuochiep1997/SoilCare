using AutoMapper;
using SoilCareWebAPI.Data;
using SoilCareWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilCareWebAPI.AutomapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserModel>();
            CreateMap<AddUserModel, User>();
            // Plant
            CreateMap<AddPlantModel, Plant>();

            // Soil
            CreateMap<Soil, SoilModel>();
            CreateMap<Soil, SoilModelDetail>()
                .ForMember(dest => dest.Plants,
                m => m.MapFrom(src => src.Plants.Select(s => s.Plant_id)));
            // Land
            CreateMap<Land, LandModel>();
            CreateMap<AddLandModel, Land>()
                .ForMember(dest => dest.Land_id, m => m.Ignore())
                .ForMember(dest => dest.Status, m => m.Ignore())
                .ForMember(dest => dest.Created_at, m => m.Ignore());

            // Measure
            CreateMap<Measure, MeasureModel>()
                .ForMember(dest => dest.Plant_name,
                    m => m.MapFrom(src => src.Plant.Plant_name))
                .ForMember(dest => dest.HasSolution,
                    m => m.MapFrom(src => src.SolutionOffers.Where(s => !s.Status.ToLower().Equals("deleted")).Count()));
            CreateMap<AddMeasureModel, Measure>();

            // SolutionOffer
            CreateMap<SolutionOffer, SolutionWithStatusModel>()
                .ForMember("Solution_name",
                    m => m.MapFrom(src => src.Solution.Solution_name))
                .ForMember("Value",
                    m => m.MapFrom(src => src.Value_config ?? src.Solution.Value))
                .ForMember("Unit_symbol",
                    m => m.MapFrom(src => src.Unit_symbol_config ?? src.Solution.Unit_symbol))
                .ForMember("Unit_name",
                    m => m.MapFrom(src => src.Unit_name_config ?? src.Solution.Unit_name))
                .ForMember("Solution_purpose",
                    m => m.MapFrom(src => src.Solution.Solution_purpose))
                .ForMember("Solution_description",
                    m => m.MapFrom(src => src.Solution.Solution_description))
                .ForMember("Owner",
                    m => m.MapFrom(src => src.Solution.Owner));
            CreateMap<SolutionOfferModel, SolutionOffer>()
                .ForMember("Status", m => m.Ignore());


            // Solution
            CreateMap<Solution, SolutionModel>();
            CreateMap<AddSolutionModel, Solution>();
			

        }

    }
}
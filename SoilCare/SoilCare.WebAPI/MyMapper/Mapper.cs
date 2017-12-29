//using SoilCare.WebAPI.Data;
//using SoilCare.WebAPI.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace SoilCare.WebAPI.MyMapper
//{
//    public class Mapper
//    {
//         Mapping UserModel with User Context
//        public UserModel Create(User user)
//        {
//            return new UserModel()
//            {
//                User_id = user.User_id,
//                User_name = user.User_name,
//                User_image = user.User_image,
//                Telephone = user.Telephone,
//                Region = user.Region,
//                Created_at = user.Created_at,
//                Lands = user.Lands.Select(s => Create(s))
//            };
//        }
//        public PlantModel Create(Plant plant)
//        {
//            return new PlantModel
//            {

//            };
//        }
//        public SoilModel Create(Soil plant)
//        {
//            return new SoilModel
//            {
//                Soil_id = plant.Soil_id,
//                Soil_name = plant.Soil_name,
//                Min_nutrient = plant.Min_nutrient,
//                Max_nutrient = plant.Max_nutrient,
//                Min_acidity = plant.Min_acidity,
//                Max_acidity = plant.Max_acidity,
//                Min_humidity = plant.Min_humidity,
//                Max_humidity = plant.Max_humidity,
//                Min_porosity = plant.Min_porosity,
//                Max_porosity = plant.Max_porosity,
//                Min_salinity = plant.Min_salinity,
//                Max_salinity = plant.Max_salinity,
//                Min_water_retention = plant.Min_water_retention,
//                Max_water_retention = plant.Max_water_retention,
//                Plants = plant.Plants.Select(p => p.Plant_id),
//            };
//        }
//         Mapping LandModel with Land Context
//        public LandModel Create(Land land)
//        {
//            return new LandModel()
//            {
//                Land_id = land.Land_id,
//                Land_name = land.Land_name,
//                Land_address = land.Land_address,
//                Land_image = land.Land_image,
//                Created_at = land.Created_at,
//                Measures = land.Measures.Select(s => Create(s))
//            };
//        }
//         Mapping MeasureModel with Measure Context
//        public MeasureModel Create(Measure measure)
//        {
//            return new MeasureModel()
//            {
//                Measure_id = measure.Measure_id,
//                Created_at = measure.Created_at,
//                Plant_name = measure.Plant.Plant_name,
//                HasSolution = measure.hasSolution,
//                Rate = measure.Rate,
//                Nutrient = measure.Nutrient,
//                Humidity = measure.Humidity,
//                Acidity = measure.Acidity,
//                Salinity = measure.Salinity,
//                Porosity = measure.Porosity,
//                Water_retention = measure.Water_retention,
//                Solution = measure.SolutionOffers.Select(s => Create(s)),
//            };
//        }

//        public SolutionWithStatusModel Create(SolutionOffer p)
//        {
//            return new SolutionWithStatusModel
//            {
//                Solution_id = p.Solution_id,
//                Solution_name = p.Solution.Solution_name,
//                 //if user configed Values and Units
//                 //query from SolutionOffer table
//                 //otherwise query from Solution table
//                Value = p.Value_config ?? p.Solution.Value,
//                Unit_symbol = p.Unit_symbol_config ?? p.Solution.Unit_symbol,
//                Unit_name = p.Unit_name_config ?? p.Solution.Unit_name,
//                Quantity = p.Quantity_config ?? p.Solution.Quantity,
//                Solution_purpose = p.Solution.Solution_purpose,
//                Solution_discription = p.Solution.Solution_discription,
//                Owner = p.Solution.Owner,
//                Status = p.Status
//            };
//        }

//         Mapping SolutionModel with Solution Context
//        public SolutionModel Create (Solution solution)
//        {
//            return new SolutionModel
//            {
//                Solution_id = solution.Solution_id,
//                Solution_name = solution.Solution_name,
//                Value = solution.Value,
//                Unit_symbol = solution.Unit_symbol,
//                Unit_name = solution.Unit_name,
//                Quantity = solution.Quantity,
//                Solution_purpose = solution.Solution_purpose,
//                Solution_discription = solution.Solution_discription,
//                Owner = solution.Owner,
//            };
//        }
//    }
//}
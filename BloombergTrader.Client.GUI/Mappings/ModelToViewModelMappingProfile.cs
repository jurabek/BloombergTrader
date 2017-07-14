using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client.GUI.Mappings
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<Settings, SettingsViewModel>()
                .ForMember(vm => vm.ServerHost, map => map.MapFrom(x => x.ServerHost))
                .ForMember(vm => vm.ServerPort, map => map.MapFrom(x => x.ServerPort));
            
        }
    }
    
}

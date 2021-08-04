using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private static List<Character> characters = new List<Character>{
      new Character(),
      new Character{Id = 1, Name = "Sam"}
    };
    private readonly IMapper _mapper;

    public CharacterService(IMapper mapper)
    {
      _mapper = mapper;
    }
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(characters);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();
      serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> Save(AddCharacterDto newCharacter)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      characters.Add(_mapper.Map<Character>(newCharacter));
      serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(characters); ;
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updateharacter)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();

      try
      {
        Character character = characters.FirstOrDefault(c => c.Id == updateharacter.Id);

        character.Name = updateharacter.Name;

        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
      {

        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }
  }
}
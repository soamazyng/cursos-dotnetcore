using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
  public interface ICharacterService
  {
    Task<ServiceResponse<List<GetCharacterDto>>> GetAll();
    Task<ServiceResponse<List<GetCharacterDto>>> Save(AddCharacterDto newCharacter);
    Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updateharacter);
    Task<ServiceResponse<GetCharacterDto>> GetById(int id);
  }
}
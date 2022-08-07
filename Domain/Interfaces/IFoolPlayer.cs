using Domain.Entities;

namespace Domain.Enums;

public interface IFoolPlayer
{
    void WithdrawCards(params Card[] cards);
    PlayerRole PlayerRole { get; set; }
    bool Confirm { get; set; }

}
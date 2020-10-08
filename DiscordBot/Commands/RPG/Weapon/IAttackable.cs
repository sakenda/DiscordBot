namespace DiscordBot.Commands.RPG
{
    interface IAttackable
    {
        int Attack();
        int Block(int attack);
        int Counter();
    }
}

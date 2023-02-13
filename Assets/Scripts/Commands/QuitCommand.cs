namespace LightingTheDungeon
{
    public struct QuitCommand : ICommand
    {
        public void Execute()
        {
            // 触发退出游戏事件
            QuitEvent.Invoke();
        }
    }
}
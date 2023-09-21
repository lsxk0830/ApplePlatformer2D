using QFramework;

namespace Blue
{
    public interface IPlayerModel : IModel
    {
        int MaxHP { get; set; }
        int HP { get; set; }
    }

    public class PlayerModel : AbstractModel, IPlayerModel
    {
        public int MaxHP { get; set; } = 1;
        public int HP { get; set; } = 1;

        protected override void OnInit()
        {

        }
    }
}
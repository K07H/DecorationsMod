namespace DecorationsMod.Controllers
{
    public class DecorativeLockerController : HandTarget, IHandTarget
    {
        private StorageContainer _storageContainer = null;

        public override void Awake()
        {
            base.Awake();
            this._storageContainer = this.gameObject.GetComponentInChildren<StorageContainer>();
        }

        public void OnHandClick(GUIHand hand)
        {
            if (!base.enabled || this._storageContainer == null)
                return;

            this._storageContainer.OnHandClick(hand);
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!base.enabled)
                return;

            HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            HandReticle.main.SetInteractText("OpenStorage");
        }
    }
}

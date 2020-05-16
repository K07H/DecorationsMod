namespace DecorationsMod.Controllers
{
    public class DecorativeLockerController : HandTarget, IHandTarget
    {
        private StorageContainer _storageContainer = null;

        public override void Awake()
        {
            base.Awake();
            StorageContainer sc;
            try { sc = this.gameObject.GetComponentInChildren<StorageContainer>(); }
            catch { sc = null; }
            if (sc != null)
                this._storageContainer = sc;
        }

        public void OnHandClick(GUIHand hand)
        {
            if (!base.enabled)
                return;

            if (this._storageContainer == null)
            {
                StorageContainer sc;
                try { sc = this.gameObject.transform.parent.GetComponentInChildren<StorageContainer>(); }
                catch { sc = null; }
                if (sc != null)
                    this._storageContainer = sc;
            }
            if (this._storageContainer != null)
                this._storageContainer.OnHandClick(hand);
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!base.enabled)
                return;

            HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
#if BELOWZERO
            HandReticle.main.SetTextRaw(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord("OpenCustomStorage"));
#else
            HandReticle.main.SetInteractText("OpenCustomStorage");
#endif
        }
    }
}

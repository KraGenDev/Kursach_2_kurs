using System.Collections;

namespace Interfaces
{
    public interface ICyclicalWeapon
    {
        public void StopUsing();

        protected abstract IEnumerator CyclicalUsing();
    }
}
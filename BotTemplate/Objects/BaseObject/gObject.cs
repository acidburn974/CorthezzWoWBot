using System;

namespace BotTemplate.Objects.BaseObject
{
    abstract class gObject
    {
        #region Address and guid
        internal uint baseAdd = 0;
        internal uint descriptor = 0;
        internal UInt64 guid = 0;

        #endregion
    }
}

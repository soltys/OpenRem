using OpenRem.Common;
using OpenRem.Config.ConfigFiles;

namespace OpenRem.Config
{
    public static class ProbeSideExtensions
    {
        public static Side ToSide(this ProbeSide probeSide)
        {
            return probeSide == ProbeSide.Right ? Side.Right : Side.Left;
        }
    }
}

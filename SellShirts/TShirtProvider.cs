using System.Collections.Immutable;

namespace SellShirts
{
    internal class TShirtProvider
    {
        // Collection of Shirts can never change... stored as ImmutableArray
        public static ImmutableArray<TShirt> AllShirts { get; } = ImmutableArray.Create(
            new TShirt("igeek", "IGeek", 500),
            new TShirt("bigdata", "Big Data", 600),
            new TShirt("ilovenode", "I Love Node", 750),
            new TShirt("kcdc", "kcdc", 400),
            new TShirt("docker", "Docker", 350),
            new TShirt("qcon", "QCon", 300),
            new TShirt("ps", "PluralSight", 60000),
            new TShirt("pslive", "PluralSight Live", 60000)
            );
              

        public static TShirt SelectRandomShirt()
        {
            int selectedIndex = Rnd.NextInt(AllShirts.Length);
            return AllShirts[selectedIndex];
        }


        public static ImmutableDictionary<string, TShirt> AllShirtsByCode { get; } = AllShirts.ToImmutableDictionary(x => x.Code);
    }
}
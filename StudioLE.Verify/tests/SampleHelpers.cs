using Newtonsoft.Json;
using StudioLE.Verify.Json;
using StudioLE.Verify.Tests.Mock;

namespace StudioLE.Verify.Tests;

internal sealed class SampleHelpers
{
    public static BBox3[] GetValidSample()
    {
        MockVerify verify = new("JsonVerifier_Pass");
        string actualJson = verify.ReadSourceFile(".json");
        BBox3[]? deserialized = JsonConvert.DeserializeObject<BBox3[]>(actualJson, JsonVerifier.Converters);
        return deserialized ?? throw new("Failed to de-serialize.");
    }

    public static BBox3[] GetInvalidSample()
    {
        MockVerify verify = new("JsonVerifier_Fail");
        string actualJson = verify.ReadSourceFile(".json");
        BBox3[]? deserialized = JsonConvert.DeserializeObject<BBox3[]>(actualJson, JsonVerifier.Converters);
        return deserialized ?? throw new("Failed to de-serialize.");
    }
}

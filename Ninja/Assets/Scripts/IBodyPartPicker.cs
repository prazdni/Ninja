public interface IBodyPartPicker
{
    void Pick(BodyPart bodyPart);
    void Unpick(BodyPart bodyPart, bool killed);
}

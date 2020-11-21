namespace ExtendedModelProperties
{
    public class DragDropAsset
    {
        public enum AssetType
        {
            Animation = 0,
            Font,
            Material,
            Model,
            Texture
        }

        public AssetType Type { get; set; }

        public string FileName { get; set; }
    }
}

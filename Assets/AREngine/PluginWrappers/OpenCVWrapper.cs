using System.Runtime.InteropServices;
internal static class OpenCVWrapper {
	[DllImport("OpenCVPlugin")]
	internal static extern int Init(ref int outCamereWidth, ref int outCameraHeight);
	[DllImport("OpenCVPlugin")]
	internal static extern int Close();
	[DllImport("OpenCVPlugin")]
	internal static extern void SetScale(int downscale);
	[DllImport("OpenCVPlugin")]
	internal unsafe static extern void Detect(CvCircle* outFaces, int maxOutFacesCount, ref int outDetectFacesCount);
	
	[StructLayout(LayoutKind.Sequential, Size=12)]
	public struct CvCircle
	{
		public int X, Y, Radius;
	}
}

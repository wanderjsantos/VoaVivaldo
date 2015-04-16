using UnityEngine;
using UnityEditor;

// /////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Batch audio import settings modifier.
//
// Modifies all selected audio clips in the project window and applies the requested modification on the
// audio clips. Idea was to have the same choices for multiple files as you would have if you open the
// import settings of a single audio clip. Put this into Assets/Editor and once compiled by Unity you find
// the new functionality in Custom -> Sound. Enjoy! :-)
//
// April 2010. Based on Martin Schultz's texture import settings batch modifier.
//
// /////////////////////////////////////////////////////////////////////////////////////////////////////////
public class AudioImportSettingsUtils : ScriptableObject {
	
	[MenuItem( "Utils/Audio/Alterar Import settings")]
	public static void Set()
	{
		SetAudioImportSettings( true, AudioImporterFormat.Native, 90, AudioImporterLoadType.StreamFromDisc, false, false );
	}
		
				
	static void SetAudioImportSettings ( bool enableHardwareDecoding = true, AudioImporterFormat newFormat = AudioImporterFormat.Native,
	                                    int newCompressionBitrate = 90, AudioImporterLoadType decompressOnLoadType = AudioImporterLoadType.StreamFromDisc,
	                                    bool is3D = false, bool forceToMono = false
										  )
	{
		Object[] audioclips = GetSelectedAudioclips();
		Selection.objects = new Object[0];
		foreach (AudioClip audioclip in audioclips) {
			string path = AssetDatabase.GetAssetPath(audioclip);
			AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
			audioImporter.hardware = enableHardwareDecoding;
			audioImporter.format = newFormat;
			audioImporter.compressionBitrate = newCompressionBitrate;
			audioImporter.loadType = decompressOnLoadType;
			audioImporter.threeD = is3D;
			audioImporter.forceToMono = forceToMono;
			
			AssetDatabase.ImportAsset(path);
		}
	}
	
	static Object[] GetSelectedAudioclips()
	{
		return Selection.GetFiltered(typeof(AudioClip), SelectionMode.DeepAssets);
	}
}
using UnityEngine;
using System.Collections;
using ZXing;

public class ScanQRCode : MonoBehaviour {

    private const int CHECK_FREQUENCY = 5;

    ARController arCtl;
    

    public bool btnTest;
    public TextMesh text;
    public GameObject attachPoint;

    private int frameConuter;

    void Awake()
    {
        arCtl = GetComponent<ARController>();
        frameConuter = 0;
    }

	// Use this for initialization
	void Start () {
	
	}

    bool OnButton(ref bool btnIndicator)
    {
        if (btnIndicator)
        {
            btnIndicator = false;
            return true;
        }

        return false;
    }
	
	// Update is called once per frame
	void Update () {
        frameConuter++;
	    //if (OnButton(ref btnTest))
        if (frameConuter % CHECK_FREQUENCY == 0)
        {
            Test();
        }

        GameObject videoSource = GameObject.Find("Video source 0");
        if (videoSource != null && videoSource.transform.parent == null)
        {
            videoSource.transform.parent = attachPoint.transform;
        }

    }

    void Test()
    {
        //Debug.Log(arCtl._videoColor32Array0);
        //Debug.Log(arCtl._videoColorArray0);
        //Debug.Log(arCtl._videoTexture0);
        if (arCtl._videoTexture0 == null)
            return;

        
        // create a barcode reader instance
        IBarcodeReader reader = new BarcodeReader();
        // load a bitmap
        var barcodeBitmap = arCtl._videoTexture0.GetRawTextureData();// (Bitmap)Bitmap.LoadFrom("C:\\sample-barcode-image.png");
        // detect and decode the barcode inside the bitmap
        var result = reader.Decode(barcodeBitmap, arCtl._videoTexture0.width, arCtl._videoTexture0.height, RGBLuminanceSource.BitmapFormat.ARGB32);
        // do something with the result
        if (result != null)
        {
            string txtDecoderType = result.BarcodeFormat.ToString();
            string txtDecoderContent = result.Text;

            Debug.Log("txtDecoderType = " + txtDecoderType);
            Debug.Log("txtDecoderContent = " + txtDecoderContent);
            text.text = txtDecoderContent;
        }
        else
        {
            //Debug.Log("QRScan: null result");
        }

    }
}

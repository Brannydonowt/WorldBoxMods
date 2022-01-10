using System;
using System.IO;
using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using RSG;
using SAES;
using UnityEngine;

// Token: 0x0200021D RID: 541
public class S3Manager : MonoBehaviour
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00076E18 File Offset: 0x00075018
	private IAmazonS3 Client
	{
		get
		{
			if (this._sClient == null)
			{
				try
				{
					SAES saes = new SAES();
					this._abnn = saes.ToString("Js23DGKu7RMNik4XECoQVkNFIW//dsZNcfyKb49RlFU");
					this._sClient = new AmazonS3Client(saes.ToString("VvrCEe1TcUBvQeiSelndpl1Plc4FoMxddSglHA2Fe0M"), saes.ToString("WVbbIlYTAH37Glxvl1MSDpKPffhczwdbi5FRgkSs8mkLEuLzE6YCiouHH71vVgLS"), RegionEndpoint.USEast2);
					saes.init(false);
				}
				catch (Exception ex)
				{
					Debug.LogError("s3 manager not working");
					Debug.LogError(ex.Message);
				}
			}
			return this._sClient;
		}
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00076EA0 File Offset: 0x000750A0
	private void Start()
	{
		S3Manager.instance = this;
		Config.uploadAvailable = false;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00076EB0 File Offset: 0x000750B0
	public Promise<string> UploadFileToAWS3(string pFileName, byte[] pFileRawBytes)
	{
		Promise<string> promise = new Promise<string>();
		try
		{
			MemoryStream inputStream = new MemoryStream(pFileRawBytes);
			PutObjectRequest putObjectRequest = new PutObjectRequest
			{
				BucketName = this._abnn,
				Key = pFileName,
				InputStream = inputStream,
				CannedACL = S3CannedACL.Private
			};
			this.Client.PutObjectAsync(putObjectRequest, delegate(AmazonServiceResult<PutObjectRequest, PutObjectResponse> responseObj)
			{
				if (responseObj.Exception == null)
				{
					promise.Resolve(responseObj.Request.Key);
					return;
				}
				promise.Reject(new Exception("Error when uploading!"));
			}, null);
		}
		catch (WebException ex)
		{
			using (Stream responseStream = ex.Response.GetResponseStream())
			{
				using (StreamReader streamReader = new StreamReader(responseStream))
				{
					Debug.Log(streamReader.ReadToEnd());
				}
			}
			promise.Reject(ex);
		}
		catch (Exception ex2)
		{
			promise.Reject(ex2);
		}
		return promise;
	}

	// Token: 0x04000E7F RID: 3711
	public static S3Manager instance;

	// Token: 0x04000E80 RID: 3712
	private const string _abn = "Js23DGKu7RMNik4XECoQVkNFIW//dsZNcfyKb49RlFU";

	// Token: 0x04000E81 RID: 3713
	private const string _aak = "VvrCEe1TcUBvQeiSelndpl1Plc4FoMxddSglHA2Fe0M";

	// Token: 0x04000E82 RID: 3714
	private const string _ask = "WVbbIlYTAH37Glxvl1MSDpKPffhczwdbi5FRgkSs8mkLEuLzE6YCiouHH71vVgLS";

	// Token: 0x04000E83 RID: 3715
	private string _abnn;

	// Token: 0x04000E84 RID: 3716
	private IAmazonS3 _sClient;
}

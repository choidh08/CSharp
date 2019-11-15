using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace coinBlock.Utility
{
    public class NetDriveLib
    {
        /// <summary>
        /// 네트워크 리소스
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NetworkResource
        { 
            /// <summary>
            /// 범위
            /// </summary>
            public uint Scope;

            /// <summary>
            /// 타입
            /// </summary>
            public uint Type;

            /// <summary>
            /// 표시 타입
            /// </summary>
            public uint DisplayType;

            /// <summary>
            /// 용법
            /// </summary>
            public uint Usage;

            /// <summary>
            /// 로컬명
            /// </summary>
            public string LocalName;

            /// <summary>
            /// 원격명
            /// </summary>
            public string RemoteName;

            /// <summary>
            /// 주석
            /// </summary>
            public string Comment;

            /// <summary>
            /// 제공자
            /// </summary>
            public string Provider;
        }

        /// <summary>
        /// 네트워크 드라이브 연결하기
        /// </summary>
        /// <param name="ownerWindowHandle">소유자 윈도우 핸들</param>
        /// <param name="networkResource">네트워크 리소스</param>
        /// <param name="password">패스워드</param>
        /// <param name="userID">사용자 ID</param>
        /// <param name="flag">플래그</param>
        /// <param name="accessNameStringBuilder">액세스명 StringBuilder</param>
        /// <param name="bufferSize">버퍼 크기</param>
        /// <param name="result">결과 코드</param>
        /// <returns>처리 결과</returns>
        [DllImport("mpr.dll", CharSet = CharSet.Auto)]
        public static extern int WNetUseConnection
        (
            IntPtr ownerWindowHandle,
            [MarshalAs(UnmanagedType.Struct)] ref NetworkResource networkResource,
            string password,
            string userID,
            uint flag,
            StringBuilder accessNameStringBuilder,
            ref int bufferSize,
            out uint result
        );

        #region 네트워크 드라이브 연결하기 - ConnectNetworkDrive(networkDrive, shareFolder, userID, password)
        /// <summary>
        /// 네트워크 드라이브 연결하기
        /// </summary>
        /// <param name="networkDrive">네트워크 드라이브</param>
        /// <param name="shareFolder">공유 폴더</param>
        /// <param name="userID">사용자 ID</param>
        /// <param name="password">패스워드</param>
        /// <returns>
        /// 처리 결과
        /// 정상 : 0
        /// 오류 : 0이 아닌 값
        ///        - 1203 (공유 폴더 경로 오류)
        ///        - 1326 (사용자/패스워드 불일치)
        /// </returns>
        public int ConnectNetworkDrive(string networkDrive, string shareFolder, string userID, string password)
        {
            NetworkResource networkResource = new NetworkResource();

            networkResource.Type = 1;
            networkResource.LocalName = networkDrive;
            networkResource.RemoteName = shareFolder;
            networkResource.Provider = null;

            uint flag = 0u;
            int bufferSize = 64;
            StringBuilder stringBuilder = new StringBuilder(bufferSize);
            uint result = 0u;

            return WNetUseConnection
            (
                IntPtr.Zero,
                ref networkResource,
                password,
                userID,
                flag,
                stringBuilder,
                ref bufferSize,
                out result
            );
        }
        #endregion
    }
}


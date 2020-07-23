using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuikApp
{
  public class Quik
  {
    public const string DLL_NAME = "TRANS2QUIK.DLL";
    #region Константы возвращаемых значений
    public const Int32 TRANS2QUIK_SUCCESS = 0;
    public const Int32 TRANS2QUIK_FAILED = 1;
    public const Int32 TRANS2QUIK_QUIK_TERMINAL_NOT_FOUND = 2;
    public const Int32 TRANS2QUIK_DLL_VERSION_NOT_SUPPORTED = 3;
    public const Int32 TRANS2QUIK_ALREADY_CONNECTED_TO_QUIK = 4;
    public const Int32 TRANS2QUIK_WRONG_SYNTAX = 5;
    public const Int32 TRANS2QUIK_QUIK_NOT_CONNECTED = 6;
    public const Int32 TRANS2QUIK_DLL_NOT_CONNECTED = 7;
    public const Int32 TRANS2QUIK_QUIK_CONNECTED = 8;
    public const Int32 TRANS2QUIK_QUIK_DISCONNECTED = 9;
    public const Int32 TRANS2QUIK_DLL_CONNECTED = 10;
    public const Int32 TRANS2QUIK_DLL_DISCONNECTED = 11;
    public const Int32 TRANS2QUIK_MEMORY_ALLOCATION_ERROR = 12;
    public const Int32 TRANS2QUIK_WRONG_CONNECTION_HANDLE = 13;
    public const Int32 TRANS2QUIK_WRONG_INPUT_PARAMS = 14;
    #endregion

    public static string ResultToString(Int32 Result)
    {
      switch (Result)
      {
        case TRANS2QUIK_SUCCESS:                                //0
          return "TRANS2QUIK_SUCCESS";
        case TRANS2QUIK_FAILED:                                 //1
          return "TRANS2QUIK_FAILED";
        case TRANS2QUIK_QUIK_TERMINAL_NOT_FOUND:                //2
          return "TRANS2QUIK_QUIK_TERMINAL_NOT_FOUND";
        case TRANS2QUIK_DLL_VERSION_NOT_SUPPORTED:              //3
          return "TRANS2QUIK_DLL_VERSION_NOT_SUPPORTED";
        case TRANS2QUIK_ALREADY_CONNECTED_TO_QUIK:              //4
          return "TRANS2QUIK_ALREADY_CONNECTED_TO_QUIK";
        case TRANS2QUIK_WRONG_SYNTAX:                           //5
          return "TRANS2QUIK_WRONG_SYNTAX";
        case TRANS2QUIK_QUIK_NOT_CONNECTED:                     //6
          return "TRANS2QUIK_QUIK_NOT_CONNECTED";
        case TRANS2QUIK_DLL_NOT_CONNECTED:                      //7
          return "TRANS2QUIK_DLL_NOT_CONNECTED";
        case TRANS2QUIK_QUIK_CONNECTED:                         //8
          return "TRANS2QUIK_QUIK_CONNECTED";
        case TRANS2QUIK_QUIK_DISCONNECTED:                      //9
          return "TRANS2QUIK_QUIK_DISCONNECTED";
        case TRANS2QUIK_DLL_CONNECTED:                          //10
          return "TRANS2QUIK_DLL_CONNECTED";
        case TRANS2QUIK_DLL_DISCONNECTED:                       //11
          return "TRANS2QUIK_DLL_DISCONNECTED";
        case TRANS2QUIK_MEMORY_ALLOCATION_ERROR:                //12
          return "TRANS2QUIK_MEMORY_ALLOCATION_ERROR";
        case TRANS2QUIK_WRONG_CONNECTION_HANDLE:                //13
          return "TRANS2QUIK_WRONG_CONNECTION_HANDLE";
        case TRANS2QUIK_WRONG_INPUT_PARAMS:                     //14
          return "TRANS2QUIK_WRONG_INPUT_PARAMS";
        default:
          return "UNKNOWN_VALUE";
      }
    }

    public static string ByteToString(byte[] Str)
    {
      int count = 0;
      for (int i = 0; i < Str.Length; ++i)
      {
        if (0 == Str[i])
        {
          count = i;
          break;
        }
      }
      return System.Text.Encoding.Default.GetString(Str, 0, count);
    }

    public static string DateTimeStr(System.Runtime.InteropServices.ComTypes.FILETIME filetime)
    {
      long high = filetime.dwHighDateTime;
      long ft = (high << 32) + filetime.dwLowDateTime;

      return DateTime.FromFileTimeUtc(ft).ToString();
    }
    public string LastEMsg { get; set; }
    public Quik()
    {
    }

    [DllImport(DLL_NAME, EntryPoint = "TRANS2QUIK_CONNECT", CallingConvention = CallingConvention.StdCall)]
    static extern Int32 connect(string lpcstrConnectionParamsString, ref Int32 pnExtendedErrorCode, byte[] lpstrErrorMessage, UInt32 dwErrorMessageSize);

    public bool connect(string PathQuik)
    {
      Byte[] EMsg = new Byte[50];
      UInt32 EMsgSz = 50;
      Int32 ExtEC = 0, rez = -1;
      rez = connect(PathQuik, ref ExtEC, EMsg, EMsgSz);
      LastEMsg = ByteToString(EMsg);
      return rez == TRANS2QUIK_SUCCESS;
    }

    [DllImport(DLL_NAME, EntryPoint = "TRANS2QUIK_IS_DLL_CONNECTED", CallingConvention = CallingConvention.StdCall)]
    static extern Int32 is_dll_connected(ref Int32 pnExtendedErrorCode, byte[] lpstrErrorMessage, UInt32 dwErrorMessageSize);

    public bool is_dll_connected()
    {
      Byte[] EMsg = new Byte[50];
      UInt32 EMsgSz = 50;
      Int32 ExtEC = 0, rez = -1;
      rez = is_dll_connected(ref ExtEC, EMsg, EMsgSz) & 255;
      LastEMsg = ByteToString(EMsg);
      return rez == TRANS2QUIK_DLL_CONNECTED;
    }

    [DllImport(DLL_NAME, EntryPoint = "TRANS2QUIK_IS_QUIK_CONNECTED", CallingConvention = CallingConvention.StdCall)]
    static extern Int32 is_quik_connected(ref Int32 pnExtendedErrorCode, byte[] lpstrErrorMessage, UInt32 dwErrorMessageSize);

    public bool is_quik_connected()
    {
      Byte[] EMsg = new Byte[50];
      Int32 ExtEC = 0, rez = -1;
      UInt32 EMsgSz = 50;
      rez = is_quik_connected(ref ExtEC, EMsg, EMsgSz) & 255;
      LastEMsg = ByteToString(EMsg);
      return rez == TRANS2QUIK_QUIK_CONNECTED;
    }

    [DllImport(DLL_NAME, EntryPoint = "TRANS2QUIK_SEND_SYNC_TRANSACTION", CallingConvention = CallingConvention.StdCall)]
    static extern Int32 send_sync_transaction(
      string lpstTransactionString,
      ref Int32 pnReplyCode,
      ref int pdwTransId,
      ref UInt64 pnOrderNum,
      byte[] lpstrResultMessage,
      UInt32 dwResultMessageSize,
      ref Int32 pnExtendedErrorCode,
      byte[] lpstrErrorMessage,
      UInt32 dwErrorMessageSize
    );

    public int send_sync_transaction(string transactionStr, ref Int32 ReplyCd, ref int TransId, ref UInt64 OrderNum, ref string ResultMessage)
    {
      Byte[] EMsg = new Byte[100];
      Byte[] ResMsg = new Byte[100];
      Int32 ExtEC = -100;
      UInt32 ResMsgSz = 100, EMsgSz = 100;
      var rez = send_sync_transaction(transactionStr, ref ReplyCd, ref TransId, ref OrderNum, ResMsg, ResMsgSz, ref ExtEC, EMsg, EMsgSz);
      ResultMessage = ByteToString(ResMsg).Trim();
      LastEMsg = ByteToString(EMsg);
      return rez;
    }

    [DllImport(DLL_NAME, EntryPoint = "TRANS2QUIK_DISCONNECT", CallingConvention = CallingConvention.StdCall)]
    static extern Int32 disconnect(ref Int32 pnExtendedErrorCode, byte[] lpstrErrorMessage, UInt32 dwErrorMessageSize);
    public bool disconnect()
    {
      Byte[] EMsg = new Byte[50];
      UInt32 EMsgSz = 50;
      Int32 ExtEC = 0;
      var rez = disconnect(ref ExtEC, EMsg, EMsgSz);
      LastEMsg = ByteToString(EMsg);
      return rez == TRANS2QUIK_SUCCESS;
    }

    public delegate void connection_status_callback(Int32 nConnectionEvent, UInt32 nExtendedErrorCode, byte[] lpstrInfoMessage);

    [DllImport(DLL_NAME, EntryPoint = "TRANS2QUIK_SET_CONNECTION_STATUS_CALLBACK", CallingConvention = CallingConvention.StdCall)]
    public static extern Int32 set_connection_status_callback(
      connection_status_callback pfConnectionStatusCallback,
      UInt32 pnExtendedErrorCode,
      byte[] lpstrErrorMessage,
      UInt32 dwErrorMessageSize
    );

    public delegate void transaction_reply_callback(Int32 nTransactionResult, Int32 nTransactionExtendedErrorCode, Int32 nTransactionReplyCode, UInt32 dwTransId, UInt64 dOrderNum, [MarshalAs(UnmanagedType.LPStr)] string TransactionReplyMessage, IntPtr pTransReplyDescriptor);

    [DllImport(DLL_NAME, EntryPoint = "TRANS2QUIK_SET_TRANSACTIONS_REPLY_CALLBACK", CallingConvention = CallingConvention.StdCall)]
    public static extern Int32 set_transaction_reply_callback(
      transaction_reply_callback pfTransactionReplyCallback,
      ref Int32 pnExtendedErrorCode,
      byte[] lpstrErrorMessage,
      UInt32 dwErrorMessageSize
    );

  }
}

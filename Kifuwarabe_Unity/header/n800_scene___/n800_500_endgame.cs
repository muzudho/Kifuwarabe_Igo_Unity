﻿namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n800_scene___
{

    //--------------------------------------------------------------------------------
    // enum まとめ
    //--------------------------------------------------------------------------------

    // 死活情報でセットする値
    // その位置の石が「活」か「死」か。不明な場合は「活」で。
    // その位置の点が「黒地」「白地」「ダメ」か。
    public enum GtpStatusType {
        GTP_ALIVE,              // 0: 活
        GTP_DEAD,               // 1: 死
        GTP_ALIVE_IN_SEKI,      // 2: セキで活（未使用、「活」で代用して下さい）
        GTP_WHITE_TERRITORY,    // 3: 白地
        GTP_BLACK_TERRITORY,    // 4: 黒地
        GTP_DAME                // 5: ダメ
    }
}
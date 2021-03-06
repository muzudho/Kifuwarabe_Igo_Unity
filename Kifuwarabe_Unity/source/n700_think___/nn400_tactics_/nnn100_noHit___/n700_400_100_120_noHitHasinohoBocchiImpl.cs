﻿using Grayscale.Kifuwarabe_Igo_Unity_Think.n190_board___;//.Board;
using Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn100_noHit___;//.NoHitHasinohoBocchi;


namespace Grayscale.Kifuwarabe_Igo_Unity_Think.n700_think___.nn400_tactics_.nnn100_noHit___
{ 
    public class NoHitHasinohoBocchiImpl : NoHitHasinohoBocchi
    {
        public NoHitHasinohoBocchiImpl()
        {
            //this.m_isBocchi_ = false;
            //this.m_isSoto_ = false;
            //this.m_isEdge_ = false;
            //this.m_isCorner_ = false;
        }


        /// <summary>
        /// ぼっち石なら真。
        /// </summary>
        private bool m_isBocchi_;
        public bool IsBocchi()
        {
            return this.m_isBocchi_;
        }
        public void SetBocchi(bool value)
        {
            this.m_isBocchi_ = value;
        }

        /// <summary>
        /// 盤外なら真。
        /// </summary>
        private bool m_isSoto_;
        public bool IsSoto()
        {
            return this.m_isSoto_;
        }
        public void SetSoto(bool value)
        {
            this.m_isSoto_ = value;
        }

        /// <summary>
        /// 枠に隣接しているなら真。
        /// </summary>
        private bool m_isEdge_;
        public bool IsEdge()
        {
            return this.m_isEdge_;
        }
        public void SetEdge(bool value)
        {
            this.m_isEdge_ = value;
        }

        /// <summary>
        /// 角なら真。
        /// </summary>
        private bool m_isCorner_;
        public bool IsCorner()
        {
            return this.m_isCorner_;
        }
        public void SetCorner(bool value)
        {
            this.m_isCorner_ = value;
        }


        public void Research(
            int node,
            Board board
        ){

            this.SetSoto( false);
            this.SetEdge( false);
            this.SetCorner( false);



            this.SetBocchi( true);
            board.ForeachArroundNodes(node, (int adjNode, ref bool isBreak) =>{
                Color adjColor = board.ValueOf(adjNode);        // その色

                if (adjColor == Color.Black || adjColor == Color.White)
                {
                    // ぼっちではない。
                    this.SetBocchi( false);
                    isBreak = true;
                    goto gt_Next;
                }

                gt_Next:
                ;
            });


            int x, y;
            AbstractTable<Color>.ConvertToXy(out x, out y, node);

            if (x < 1 || board.GetBoardSize() < x ||
                y < 1 || board.GetBoardSize() < y
                )
            {
                // 盤外
                //System.Console.WriteLine(string.Format("({0:D},{1:D}) ban={2:D} ; Soto \n", x, y, boardSize));
                this.SetSoto( true);
                goto gt_EndMethod;
            }

            if (x == 1 || board.GetBoardSize() == x ||
                y == 1 || board.GetBoardSize() == y
            )
            {
                // 辺
                //System.Console.WriteLine(string.Format("({0:D},{1:D}) ban={2:D} ; EDGE \n", x, y, boardSize));
                this.SetEdge( true);
            }
            else
            {
                //System.Console.WriteLine(string.Format("({0:D},{1:D}) ban={2:D} ; ------ \n", x, y, boardSize));
                goto gt_EndMethod;
            }

            if ((x == 1 || board.GetBoardSize() == x) &&
                (y == 1 || board.GetBoardSize() == y)
            )
            {
                // 角
                //System.Console.WriteLine(string.Format("({0:D},{1:D}) ban={2:D} ; CORNER \n", x, y, boardSize));
                this.SetCorner( true);
            }

            gt_EndMethod:
            ;
        }

        // 評価値を出します。
        public int Evaluate(
            )
        {
            int score = 100;

//# ifndef RANDOM_MOVE_ONLY

            if (!this.IsBocchi())
            {
                // ぼっち石でなければ、気にせず　端でも角でも置きます。
                goto gt_EndMethod;
            }

            if (this.IsCorner())
            {
                // 角に　ぼっち石　を置きたくない。
                score -= 50;
                goto gt_EndMethod;
            }

            if (this.IsEdge())
            {
                // 辺に　ぼっち石　を置きたくない。
                score -= 33;
                goto gt_EndMethod;
            }

            gt_EndMethod:

//#endif

            return score;
        }
    }
}
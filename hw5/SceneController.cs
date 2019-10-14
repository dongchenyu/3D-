using UnityEngine;
using System.Collections;
using Com.Mygame;

namespace Com.Mygame
{
    public interface IUserInterface
    {
        void emitDisk();
    }

    public interface IQueryStatus
    {
        bool isTiming();
        bool isShooting();
        int getRound();
        int getPoint();
        int getEmitTime();
    }

    public interface IJudgeEvent
    {
        void nextRound();
        void setPoint(int point);
    }

    public class SceneController : System.Object, IQueryStatus, IUserInterface, IJudgeEvent
    {
        private static SceneController instance;
        private SceneControllerBC baseCode;
        private GameModel gameModel;
        private Judge judge;

        private int round_;
        private int point_;

        public static SceneController getInstance()
        {
            if (instance == null)
            {
                instance = new SceneController();
            }
            return instance;
        }

        public void setGameModel(GameModel obj){
            gameModel = obj;
        }
        internal GameModel getGameModel(){
            return gameModel;
        }
        public void setJudge(Judge obj){
            judge = obj;
        }
        internal Judge getJudge(){
            return judge;
        }
        public void setSceneControllerBC(SceneControllerBC obj){
            baseCode = obj;
        }
        internal SceneControllerBC getSceneControllerBC(){
            return baseCode;
        }
        public void emitDisk(){
            gameModel.prepareToEmitDisk();
        }
        public bool isTiming(){
            return gameModel.timing;
        }
        public bool isShooting(){
            return gameModel.shooting;
        }
        public int getRound(){
            return round_;
        }
        public int getPoint(){
            return point_;
        }
        public int getEmitTime(){
            return (int)gameModel.timeEmit + 1;
        }
        public void setPoint(int point){
            point_ = point;
        }
        public void nextRound()
        {
            if(++round_ < 4)
            {
                point_ = 0;
                baseCode.loadRoundData(round_);
            }
            
        }
    }
}
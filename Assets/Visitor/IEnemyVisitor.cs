namespace Assets.Visitor
{
    public interface IEnemyVisitor
    {
        void Visit(Ork ork);
        void Visit(Human human);
        void Visit(Elf elf);
        void Visit(Enemy enemy);
        void Visit(Vampire vampire);
    }
}

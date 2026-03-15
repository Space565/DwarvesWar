public class SkillState
{
    public Skill skill;
    public int currentCooldown;

    public SkillState(Skill skill)
    {
        this.skill = skill;
        currentCooldown = 0;
    }

    public bool IsReady()
    {
        return currentCooldown <= 0;
    }

    public void Use()
    {
        currentCooldown = skill.cooldown;
    }

    public void Tick()
    {
        if (currentCooldown > 0)
            currentCooldown--;
    }
}
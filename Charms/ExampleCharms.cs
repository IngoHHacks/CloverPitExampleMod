using CloverAPI.Classes;
using CloverAPI.Content.Charms;
using CloverAPI.Content.Strings;
using Panik;
using System.IO;
using System.Numerics;

namespace CloverPitExampleMod.Charms;

public static class ExampleCharms
{
    internal static PowerupScript.Identifier Charm1;
    internal static PowerupScript.Identifier Charm2;
    internal static PowerupScript.Identifier Charm3;
    
    internal static void RegisterCharms()
    {
        Charm1 = CharmManager.Builder("examplemod", "example1")
            .WithName("Example Charm #1")
            .WithDescription("When a spin doesn't score any Patterns, +1 [C_TICKET].")
            .WithTextureModel(Path.Combine(Plugin.ImagePath, "example_texture.png"))
            .WithScript<ExampleCharmScript1>()
            .BuildAndRegister();
            
        Charm2 = CharmManager.Builder("examplemod", "example2")
            .WithName("Example Charm #2")
            .WithDescription("When bought, the phone will ring. If the phone is already ringing, this will do nothing.")
            .WithIsInstantPowerup()
            .WithStartingPrice(PowerupScript.PRICE_HIGH)
            .WithStoreRerollChance(PowerupScript.STORE_REROLL_CHANCE_RARE)
            .WithOnEquipEvent(_ =>
            {
                if (!GameplayData.Instance._phone_abilityAlreadyPickedUp) 
                {
                    return;
                }
                PhoneScript.ResetForDeadline();
                PhoneScript.PhoneRing();
            })
            .WithExistingModel(PowerupScript.Identifier.SymbolInstant_Lemon, Path.Combine(Plugin.ImagePath, "example_retexture.png"))
            .BuildAndRegister();
        
        Charm3 = CharmManager.Builder("examplemod", "example3")
            .WithName("Example Charm #3")
            .WithDescription("Immediately earn 200% of the debt amount ([DEBT 200%]€) as a loan. You'll have to pay double of it back. When discarded or put in a drawer, pay back any remaining debt immediately. If you can't pay it back, the penalty is death. [LOAN DATA]")
            .WithStartingPrice(PowerupScript.PRICE_FREE)
            .WithStoreRerollChance(PowerupScript.STORE_REROLL_CHANCE_LEGENDARY)
            .WithScript<ExampleCharmScript3>()
            .WithCustomModel(Path.Combine(Plugin.DataPath ,"shark"))
            .BuildAndRegister();
        
        CustomStrings.Register("[LOAN DATA]", new StringFromCallable(ExampleCharmScript3.LoanDataString));
        CustomStrings.Register("[DEBT 200%]", new StringFromCallable(ExampleCharmScript3.LoanAmountString));

        LocalizationManager.Add("LOAN_SHARK_FAILED_TO_PAY", "You don't have enough coins € to pay back your loan...");
    }
}

internal class ExampleCharmScript1 : CharmScript
{
    public override void OnEquip(PowerupScript powerup)
    {
        SlotMachineScript.instance.OnScoreEvaluationEnd += Example_Charm_Effect;
    }

    public override void OnUnequip(PowerupScript powerup)
    {
        SlotMachineScript.instance.OnScoreEvaluationEnd -= Example_Charm_Effect;
    }

    private void Example_Charm_Effect()
    {
        if (SlotMachineScript.GetPatternsCount() <= 0)
        {
            GameplayData.CloverTicketsAdd(1L, true);
            PowerupScript.PlayTriggeredAnimation(ExampleCharms.Charm1);
        }
    }
}

[HarmonyPatch]
internal class ExampleCharmScript3 : CharmScript
{
    private BigInteger loanAmount
    {
        get => Data.GetBigInteger("loanAmount", 0L);
        set => Data.Set("loanAmount", value);
    }

    private bool loanActive
    {
        get => Data.Get("loanActive", false);
        set => Data.Set("loanActive", value);
    }
    
    public static ExampleCharmScript3 Instance { get; private set; }
    
    public ExampleCharmScript3()
    {
        Instance = this;
    }
    
    public override void OnEquip(PowerupScript powerup)
    {
        // OnEquip also gets called on game load
        if (this.loanActive)
        {
            return;
        }
        this.loanActive = true;
        this.loanAmount = GameplayData.DebtGet() * 2;
        GameplayData.CoinsAdd(this.loanAmount, true);
        PowerupScript.PlayTriggeredAnimation(ExampleCharms.Charm3);
    }

    public override void OnUnequip(PowerupScript powerup)
    {
        if (this.loanAmount > 0)
        {
            if (GameplayData.CoinsGet() >= this.loanAmount*2)
            {
                GameplayData.CoinsAdd(-this.loanAmount*2, false);
            }
            else
            {
                GameplayData.Instance.coins = GameplayData.CoinsGet() - this.loanAmount* 2; // Allow negative coins (CoinsAdd clamps at 0)
                DialogueScript.SetDialogue(false, "LOAN_SHARK_FAILED_TO_PAY");
                DialogueScript.instance.onDialogueClose += LoanSharkDeath;
            }
            this.loanAmount = 0;
        }
        this.loanActive = false;
    }
    
    private void LoanSharkDeath()
    {
        DialogueScript.instance.onDialogueClose -= LoanSharkDeath;
        GameplayMaster.instance.DieTry(GameplayMaster.DeathStep.lookAtTrapdoor, callLastChanceCallback: false);
    }
    
    public override void OnPutInDrawer(PowerupScript powerup)
    {
        PowerupScript.ThrowAway(ExampleCharms.Charm3, false);
    }
    
    public static string LoanAmountString()
    {
        if (PowerupScript.IsEquipped_Quick(ExampleCharms.Charm3) && Instance.loanActive)
        {
            return Instance.loanAmount.ToString();
        }
        return (GameplayData.DebtGet() * 2).ToString();
    }
    
    public static string LoanDataString()
    {
        if (PowerupScript.IsEquipped_Quick(ExampleCharms.Charm3) && Instance.loanActive)
        {
            return $"You currently owe {Instance.loanAmount * 2}€.";
        }
        return "You have no active loan.";
    }
}
namespace StressTest
{
    public static class TestManager
    {
        private static string[] ReasonsForFailure = [
            "Material fatigue under cyclic loading",
            "Excessive deflection beyond tolerance limits",
            "Welding defect in the main support structure",
            "Corrosion damage in critical sections",
            "Buckling under compressive load",
            "Crack propagation from stress concentration",
            "Thermal expansion mismatch",
            "Vibration-induced resonance failure",
            "Insufficient torsional rigidity",
            "Material impurity causing weak points",
            "Bolt connection loosening under dynamic load",
            "Surface coating delamination",
            "Heat treatment inconsistency",
            "Dimensional tolerance exceeded",
            "Residual stress from manufacturing process",
            "Fatigue crack initiation at notch",
            "Creep deformation at elevated temperature",
            "Brittle fracture at low temperature",
            "Galvanic corrosion between dissimilar metals",
            "Stress corrosion cracking",
            "Hydrogen embrittlement",
            "Overload beyond yield strength",
            "Improper alignment during assembly",
            "Inadequate cross-sectional area",
            "Localized plastic deformation",
            "Shear failure at connection points",
            "Bearing failure at support interfaces",
            "Fatigue failure at weld toe",
            "Thermal fatigue from repeated cycles",
            "Microstructural degradation",
            "Surface finish below specification",
            "Insufficient safety margin in design",
            "Dynamic load exceeding static calculations",
            "Resonance at operational frequency",
            "Material anisotropy causing directional weakness",
            "Inadequate corrosion protection",
            "Fastener failure under shear load",
            "Composite layer separation",
            "Adhesive bond failure in joints",
            "Thermal shock damage",
            "Oxidation at high temperature",
            "Wear and abrasion on moving parts",
            "Impact damage from foreign objects",
            "Insufficient stiffness for application",
            "Tensile failure at maximum load",
            "Compressive failure in slender members",
            "Torsional shear failure",
            "Fatigue life shorter than expected",
            "Quality control issue in raw material",
            "Manufacturing defect in critical component",
            "Кот пробежал и уронил всё",
            "Молоток выпал из рук",
            "Стресс не выдержали испытатели, а не объект",
            "Иная ошибка",
        ];
        public static TestCaseResult GenerateResult()
        {
            TestResult Ok;
            string reason;
            Random gen = new();
            if (gen.Next() % 2 == 0) 
            { 
                Ok = TestResult.Pass;
                reason = "Success!";
            }
            else
            {
                Ok = TestResult.Fail;
                reason = ReasonsForFailure[gen.Next() % ReasonsForFailure.Length ];
            }
            return new TestCaseResult(Ok, reason);
        }
    }
}
﻿namespace Domain.Entities;

#pragma warning disable
public class ClassAdmin : BaseEntity
{
    public int TrainingClassId { get; set; }
    public TrainingClass TrainingClass { get; set; }
    public int AdminId { get; set; }
    public User Admin { get; set; }
    public DateTime? CreationDate { get; set; }
    public int? CreatedBy { get; set; }
    public User? CreateByUser { get; set; }
    public DateTime? ModificationDate { get; set; }
    public int? ModificationBy { get; set; }
    public User? ModificationByUser { get; set; }
}
